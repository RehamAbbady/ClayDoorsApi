using Xunit;
using Moq;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.API.Controllers;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.Test.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IUsersService> _userServiceMock = new();
        private readonly Mock<ITokenService> _tokenServiceMock = new();
        private readonly Mock<ISecurityService> _securityServiceMock = new();

        [Fact]
        public async Task GenerateToken_ValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var user = new UserDto { Email = "test@test.com", PinHash = "hashedPin" };
            var authRequest = new AuthRequestDto { Email = "test@test.com", Pin = "1234" };
            _userServiceMock.Setup(service => service.GetUserDetailsByEmailAsync(authRequest.Email)).ReturnsAsync(user);
            _securityServiceMock.Setup(service => service.VerifyPin(authRequest.Pin, user.PinHash)).Returns(true);
            _tokenServiceMock.Setup(service => service.GenerateJwtToken(user)).Returns("testToken");
            var controller = new AuthController(_userServiceMock.Object, _tokenServiceMock.Object, _securityServiceMock.Object);

            // Act
            var result = await controller.GenerateToken(authRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GenerateToken_InvalidCredentials_ReturnsUnauthorizedResult()
        {
            // Arrange
            var authRequest = new AuthRequestDto { Email = "test@test.com", Pin = "1234" };
            _userServiceMock.Setup(service => service.GetUserDetailsByEmailAsync(authRequest.Email)).ReturnsAsync((UserDto)null);
            var controller = new AuthController(_userServiceMock.Object, _tokenServiceMock.Object, _securityServiceMock.Object);

            // Act
            var result = await controller.GenerateToken(authRequest);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
        [Fact]
        public async Task GenerateToken_InvalidPin_ReturnsUnauthorizedResult()
        {
            // Arrange
            var user = new UserDto { Email = "test@test.com", PinHash = "hashedPin" };
            var authRequest = new AuthRequestDto { Email = "test@test.com", Pin = "1234" };
            _userServiceMock.Setup(service => service.GetUserDetailsByEmailAsync(authRequest.Email)).ReturnsAsync(user);
            _securityServiceMock.Setup(service => service.VerifyPin(authRequest.Pin, user.PinHash)).Returns(false);
            var controller = new AuthController(_userServiceMock.Object, _tokenServiceMock.Object, _securityServiceMock.Object);

            // Act
            var result = await controller.GenerateToken(authRequest);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task GenerateToken_NullRequest_ReturnsBadRequestResult()
        {
            // Arrange
            _userServiceMock.Setup(service => service.GetUserDetailsByEmailAsync(It.IsAny<string>())).ReturnsAsync((UserDto)null);
            var controller = new AuthController(_userServiceMock.Object, _tokenServiceMock.Object, _securityServiceMock.Object);

            // Act
            var result = await controller.GenerateToken(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
