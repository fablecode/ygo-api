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
using System.Threading.Tasks;
using ygo.api.Auth;
using ygo.api.Auth.Models;
using ygo.api.Controllers;
using ygo.application.Configuration;
using ygo.tests.core;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ygo.api.unit.tests.ControllerTests.AccountsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class TokenTests
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
            var loginModel = new LoginModel();
            _sut.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = await _sut.Token(loginModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_Invalid_ModelState_Should_Return_BadRequestResult_With_Errors()
        {
            // Arrange
            const string expected = "Email is required";

            var loginModel = new LoginModel();
            _sut.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = await _sut.Token(loginModel) as BadRequestObjectResult;

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }


        [Test]
        public async Task Given_An_Email_If_User_Is_Not_Found_Should_Return_BadRequestResult_With_Errors()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "random@poo.com",
                Password = "Password"
            };

            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns((ApplicationUser) null);

            // Act
            var result = await _sut.Token(loginModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test] public async Task Given_An_Email_If_User_Is_Found_But_CheckPasswordSignInAsync_Fails_Should_Return_BadRequestResult()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "random@poo.com",
                Password = "Password"
            };

            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "random@poo.com",
            });
            _signInManager.CheckPasswordSignInAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.Failed);

            // Act
            var result = await _sut.Token(loginModel);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task Given_An_Email_If_User_Is_Found_But_CheckPasswordSignInAsync_Fails_And_User_IsLockedOut_Should_Return_BadRequestObjecttResult()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "random@poo.com",
                Password = "Password"
            };

            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "random@poo.com",
            });
            _signInManager.CheckPasswordSignInAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.LockedOut);

            // Act
            var result = await _sut.Token(loginModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_Email_If_User_Is_Found_But_CheckPasswordSignInAsync_Fails_And_User_IsNotAllowed_Should_Return_BadRequestObjecttResult()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "random@poo.com",
                Password = "Password"
            };

            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "random@poo.com",
            });
            _signInManager.CheckPasswordSignInAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.NotAllowed);

            // Act
            var result = await _sut.Token(loginModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_Email_If_User_Is_Found_And_CheckPasswordSignInAsync_Is_Successful_Should_Return_OkObjecttResult()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "random@poo.com",
                Password = "Password"
            };

            _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "random@poo.com",
            });
            _signInManager.CheckPasswordSignInAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.Success);
            _jwtSettings.Value.Returns(new JwtSettings
            {
                Key = "0123456789ABCDEF",
                Issuer = "http://test-api.com"
            });
            _userManager.GetRolesAsync(Arg.Any<ApplicationUser>()).Returns(new List<string> {"User", "Admin"});

            // Act
            var result = await _sut.Token(loginModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

    }
}