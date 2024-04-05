using FakeItEasy;
using FluentAssertions;
using HDrezka.Models.DTOs.Identity;
using HDrezka.Models;
using HDrezka.Services;
using HDrezka.Utilities.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HDrezka.Tests.Services
{
    public class AdminAuthServiceTests
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminAuthServiceTests() 
        {
            _userManager = A.Fake<UserManager<IdentityUser>>();
            _roleManager = A.Fake<RoleManager<IdentityRole>>();
        }

        [Fact]
        public async Task RegisterAdminAsync_WhenAdminDoesNotExist_ReturnsSuccess()
        {
            // Arrange
            var adminService = new AdminAuthService(_userManager, _roleManager);
            var model = new RegisterModel
            {
                Email = "admin@example.com",
                Username = "admin",
                Password = "AdminPassword123!"
            };

            A.CallTo(() => _userManager.FindByNameAsync(model.Username)).Returns(Task.FromResult<IdentityUser>(null));
            A.CallTo(() => _userManager.CreateAsync(A<IdentityUser>._, model.Password)).Returns(IdentityResult.Success);
            A.CallTo(() => _roleManager.RoleExistsAsync(UserRoles.Admin)).Returns(Task.FromResult(false));
            A.CallTo(() => _roleManager.CreateAsync(A<IdentityRole>._)).Returns(IdentityResult.Success);

            // Act
            Func<Task> act = async () => await adminService.RegisterAdminAsync(model);

            // Assert
            await act.Should().NotThrowAsync<UserRegistrationException>();
        }

        [Fact]
        public async Task RegisterAdminAsync_WhenAdminExists_ThrowsException()
        {
            // Arrange
            var adminService = new AdminAuthService(_userManager, _roleManager);
            var model = new RegisterModel
            {
                Email = "admin@example.com",
                Username = "existingadmin",
                Password = "AdminPassword123!"
            };

            A.CallTo(() => _userManager.FindByNameAsync(model.Username)).Returns(new IdentityUser());

            // Act
            Func<Task> act = async () => await adminService.RegisterAdminAsync(model);

            // Assert
            await act.Should().ThrowAsync<UserRegistrationException>().WithMessage("Admin already exists!");
        }

        [Fact]
        public async Task RegisterAdminAsync_WhenCreateAdminFails_ThrowsException()
        {
            // Arrange
            var adminService = new AdminAuthService(_userManager, _roleManager);
            var model = new RegisterModel
            {
                Email = "admin@example.com",
                Username = "admin",
                Password = "AdminPassword123!"
            };

            A.CallTo(() => _userManager.FindByNameAsync(model.Username)).Returns(Task.FromResult<IdentityUser>(null));
            A.CallTo(() => _userManager.CreateAsync(A<IdentityUser>._, model.Password)).Returns(IdentityResult.Failed());

            // Act
            Func<Task> act = async () => await adminService.RegisterAdminAsync(model);

            // Assert
            await act.Should().ThrowAsync<UserRegistrationException>().WithMessage("Failed to create admin!");
        }
    }
}
