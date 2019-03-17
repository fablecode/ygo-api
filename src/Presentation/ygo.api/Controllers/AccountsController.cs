using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ygo.api.Auth;
using ygo.api.Auth.Models;
using ygo.api.ServiceExtensions;
using ygo.application.Configuration;

namespace ygo.api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController
        (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        /// <summary>
        ///     User registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
                var userResult = await _userManager.CreateAsync(user, model.Password);
                if (userResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if(roleResult.Succeeded)
                        return Ok();

                    return BadRequest(roleResult.Errors.Descriptions());
                }

                return BadRequest(userResult.Errors.Descriptions());
            }

            return BadRequest(ModelState.Errors());
        }

        /// <summary>
        ///     Token authentication
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Token([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        // Basic Claims
                        var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, user.Id)
                        };

                        // Role Claims
                        var userRoles = await _userManager.GetRolesAsync(user);
                        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                        // Token generation
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_jwtSettings.Value.Issuer,
                            _jwtSettings.Value.Issuer,
                            claims.ToArray(),
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: creds);

                        return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
                    }

                    if (result.IsLockedOut)
                    {
                        return BadRequest("User is locked out.");
                    }

                    if (result.IsNotAllowed)
                    {
                        return BadRequest("User is not allowed.");
                    }

                    return BadRequest();

                }

                return NotFound();
            }

            return BadRequest(ModelState.Errors());
        }
    }
}