using FakeItEasy;
using FluentAssertions;
using HDrezka.Models.DTOs.Identity;
using HDrezka.Services;
using HDrezka.Utilities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HDrezka.Tests.Services
{
    public class AuthServiceTests
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public AuthServiceTests() 
        {
            _userManager = A.Fake<UserManager<IdentityUser>>();
            _configuration = A.Fake<IConfiguration>();
        }

        [Fact]
        public async Task RegisterUserAsync_WhenUserDoesNotExist_ReturnSuccess()
        {
            // Arrange
            var authService = new AuthService(_userManager, _configuration);
            var registerModel = new RegisterModel
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "TestPassword123!"
            };

            A.CallTo(() => _userManager.FindByNameAsync(registerModel.Username)).Returns(Task.FromResult<IdentityUser>(null));
            A.CallTo(() => _userManager.CreateAsync(A<IdentityUser>._, registerModel.Password)).Returns(IdentityResult.Success);

            // Act
            Func<Task> act = async () => await authService.RegisterUserAsync(registerModel);

            // Assert
            await act.Should().NotThrowAsync<UserRegistrationException>();
        }

        [Fact]
        public async Task RegisterUserAsync_WhenUserExists_ThrowsException()
        {
            // Arrange
            var authService = new AuthService(_userManager, _configuration);
            var registerModel = new RegisterModel
            {
                Email = "test@example.com",
                Username = "existinguser",
                Password = "TestPassword123!"
            };

            A.CallTo(() => _userManager.FindByNameAsync(registerModel.Username)).Returns(new IdentityUser());

            // Act
            Func<Task> act = async () => await authService.RegisterUserAsync(registerModel);

            // Assert
            await act.Should().ThrowAsync<UserRegistrationException>().WithMessage("User already exists!");
        }

        [Fact]
        public async Task RegisterUserAsync_WhenCreateUserFails_ThrowsException()
        {
            // Arrange
            var authService = new AuthService(_userManager, _configuration);
            var registerModel = new RegisterModel
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "TestPassword123!"
            };

            A.CallTo(() => _userManager.FindByNameAsync(registerModel.Username)).Returns(Task.FromResult<IdentityUser>(null));
            A.CallTo(() => _userManager.CreateAsync(A<IdentityUser>._, registerModel.Password)).Returns(IdentityResult.Failed(new IdentityError { Description = "Failed to create user" }));

            // Act
            Func<Task> act = async () => await authService.RegisterUserAsync(registerModel);

            // Assert
            await act.Should().ThrowAsync<UserRegistrationException>().WithMessage("Failed to create user!");
        }

        [Fact]
        public async Task AuthService_LoginAsync_ReturnsJwtToken()
        {
            // Arrange
            var authService = new AuthService(_userManager, _configuration);
            
            var username = "testuser";
            var password = "TestPassword123!";

            var user = new IdentityUser { UserName = username };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken("issuer", "issuer", claims);

            A.CallTo(() => _userManager.FindByNameAsync(username)).Returns(user);
            A.CallTo(() => _userManager.CheckPasswordAsync(user, password)).Returns(true);
            A.CallTo(() => _configuration["Jwt:Secret"]).Returns("your_secret_key_here");

            // Act
            var result = await authService.LoginAsync(username, password);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<JwtSecurityToken>();
        }

        [Fact]
        public async Task LoginAsync_WhenUserNotFound_ReturnsNull()
        {
            // Arrange
            var authService = new AuthService(_userManager, _configuration);
            var username = "nonexistentuser";
            var password = "TestPassword123!";

            A.CallTo(() => _userManager.FindByNameAsync(username)).Returns(Task.FromResult<IdentityUser>(null));

            // Act
            var result = await authService.LoginAsync(username, password);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_WhenPasswordIncorrect_ReturnsNull()
        {
            // Arrange
            var authService = new AuthService(_userManager, _configuration);
            var username = "testuser";
            var password = "IncorrectPassword";
            var user = new IdentityUser { UserName = username };

            A.CallTo(() => _userManager.FindByNameAsync(username)).Returns(user);
            A.CallTo(() => _userManager.CheckPasswordAsync(user, password)).Returns(false);

            // Act
            var result = await authService.LoginAsync(username, password);

            // Assert
            result.Should().BeNull();
        }
    }
}