using Xunit;
using Moq;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.API.Controllers;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.Test
{
    public class AuthControllerTests
    {
        private readonly Mock<IUsersRepository> _userRepositoryMock = new();
        private readonly Mock<ITokenService> _tokenServiceMock = new();
        private readonly Mock<ISecurityService> _securityServiceMock = new();

        [Fact]
        public async Task GenerateToken_ValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var user = new User { Email = "test@test.com", PinHash = "hashedPin" };
            var authRequest = new AuthRequestDto { Email = "test@test.com", Pin = "1234" };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(authRequest.Email)).ReturnsAsync(user);
            _securityServiceMock.Setup(service => service.VerifyPin(authRequest.Pin, user.PinHash)).Returns(true);
            _tokenServiceMock.Setup(service => service.GenerateJwtToken(user)).Returns("testToken");
            var controller = new AuthController(_userRepositoryMock.Object, _tokenServiceMock.Object, _securityServiceMock.Object);

            // Act
            var result = await controller.GenerateToken(authRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Dictionary<string, string>>(okResult.Value);
            Assert.Equal("testToken", returnValue["Token"]);
        }

        [Fact]
        public async Task GenerateToken_InvalidCredentials_ReturnsUnauthorizedResult()
        {
            // Arrange
            var authRequest = new AuthRequestDto { Email = "test@test.com", Pin = "1234" };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(authRequest.Email)).ReturnsAsync((User)null);
            var controller = new AuthController(_userRepositoryMock.Object, _tokenServiceMock.Object, _securityServiceMock.Object);

            // Act
            var result = await controller.GenerateToken(authRequest);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
