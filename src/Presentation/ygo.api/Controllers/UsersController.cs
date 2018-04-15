using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        /// <summary>
        ///     Get user by email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}", Name = "UserByEmail")]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public IActionResult Get(string email)
        {
            return StatusCode(501);
        }
    }
}