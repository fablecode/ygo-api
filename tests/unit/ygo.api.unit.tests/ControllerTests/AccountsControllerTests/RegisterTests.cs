using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ygo.api.Auth;
using ygo.api.Auth.Models;
using ygo.api.Controllers;
using ygo.application.Configuration;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.AccountsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class RegisterTests
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IOptions<JwtSettings> _jwtSettings;
        private AccountsController _sut;

        [SetUp]
        public void SetUp()
        {
            _userManager = Substitute.For<UserManager<ApplicationUser>>
            (
                Substitute.For<IUserStore<ApplicationUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<ApplicationUser>>(),
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                Substitute.For<ILookupNormalizer>(),
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<ApplicationUser>>>()
            );

            _signInManager = Substitute.For<SignInManager<ApplicationUser>>
            (
                _userManager,
                Substitute.For<IHttpContextAccessor>(),
                Substitute.For<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<ILogger<SignInManager<ApplicationUser>>>(),
                Substitute.For<IAuthenticationSchemeProvider>()
            );

            _jwtSettings = Substitute.For<IOptions<JwtSettings>>();

            _sut = new AccountsController(_userManager, _signInManager, _jwtSettings);
        }

        [Test]
        public async Task Given_An_Invalid_ModelState_Should_Return_BadRequestResult()
        {
            // Arrange
            var registerViewModel = new RegisterModel();
            _sut.ModelState.AddModelError("Username", "Username is required");

            // Act
            var result = await _sut.Register(registerViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_Invalid_ModelState_Should_Return_BadRequestResult_With_Errors()
        {
            // Arrange
            const string expected = "Username is required";

            var registerViewModel = new RegisterModel();
            _sut.ModelState.AddModelError("Username", "Username is required");

            // Act
            var result = await _sut.Register(registerViewModel) as BadRequestObjectResult;

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_A_Valid_User_If_User_Already_Exists_Should_Return_BadRequest()
        {
            // Arrange
            const string expected = "User already exists.";
            var registerViewModel = new RegisterModel
            {
                Email = "user@social.com",
                Password = "Password"
            };

            _userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Failed(new IdentityError { Description = "User already exists." }));

            // Act
            var result = await _sut.Register(registerViewModel) as BadRequestObjectResult;

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_A_Valid_User_If_AddToRole_Fails_Should_Return_BadRequestResult()
        {
            // Arrange
            const string expected = "Adding role to user failed.";

            var registerViewModel = new RegisterModel
            {
                Email = "user@social.com",
                Password = "Password"
            };

            _userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            _jwtSettings.Value.Returns(new JwtSettings { Key = "*@thu}qx)@h0-kI9%$](^l~xuFK>TL,%$EI", Issuer = "issue" });
            _userManager.GetClaimsAsync(Arg.Any<ApplicationUser>()).Returns(new List<Claim>());
            _userManager.GetRolesAsync(Arg.Any<ApplicationUser>()).Returns(new List<string>());

            _userManager.AddToRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Failed(new IdentityError { Description = "Adding role to user failed." }));

            // Act
            var result = await _sut.Register(registerViewModel) as BadRequestObjectResult;

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_A_Valid_User_Should_Return_OkResult()
        {
            // Arrange
            var registerViewModel = new RegisterModel
            {
                Email = "user@social.com",
                Password = "Password"
            };

            _userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            _jwtSettings.Value.Returns(new JwtSettings { Key = "*@thu}qx)@h0-kI9%$](^l~xuFK>TL,%$EI", Issuer = "issue" });
            _userManager.GetClaimsAsync(Arg.Any<ApplicationUser>()).Returns(new List<Claim>());
            _userManager.GetRolesAsync(Arg.Any<ApplicationUser>()).Returns(new List<string>());

            _userManager.AddToRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            // Act
            var result = await _sut.Register(registerViewModel) as OkResult;

            // Assert
            result.Should().BeOfType<OkResult>();
        }


        [Test]
        public async Task Given_A_Valid_User_Should_Invoke_CreateAsync_Once()
        {
            // Arrange
            var registerViewModel = new RegisterModel
            {
                Email = "user@social.com",
                Password = "Password"
            };

            _userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            _jwtSettings.Value.Returns(new JwtSettings { Key = "*@thu}qx)@h0-kI9%$](^l~xuFK>TL,%$EI", Issuer = "issue" });
            _userManager.GetClaimsAsync(Arg.Any<ApplicationUser>()).Returns(new List<Claim>());
            _userManager.GetRolesAsync(Arg.Any<ApplicationUser>()).Returns(new List<string>());

            _userManager.AddToRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            // Act
            await _sut.Register(registerViewModel);

            // Assert
            await _userManager.Received(1).CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>());
        }

        [Test]
        public async Task Given_A_Valid_User_Should_Invoke_AddToRoleAsync_Once()
        {
            // Arrange
            var registerViewModel = new RegisterModel
            {
                Email = "user@social.com",
                Password = "Password"
            };

            _userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            _jwtSettings.Value.Returns(new JwtSettings { Key = "*@thu}qx)@h0-kI9%$](^l~xuFK>TL,%$EI", Issuer = "issue" });
            _userManager.GetClaimsAsync(Arg.Any<ApplicationUser>()).Returns(new List<Claim>());
            _userManager.GetRolesAsync(Arg.Any<ApplicationUser>()).Returns(new List<string>());

            _userManager.AddToRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            // Act
            await _sut.Register(registerViewModel);

            // Assert
            await _userManager.Received(1).AddToRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>());
        }

    }
}
