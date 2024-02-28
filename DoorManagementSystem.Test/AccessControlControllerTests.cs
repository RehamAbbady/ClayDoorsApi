using Xunit;
using Moq;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.API.Controllers;
using DoorManagementSystem.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoorManagementSystem.API.Models;

namespace DoorManagementSystem.Test
{
    public class AccessControlControllerTests
    {
        private readonly Mock<IAccessControlService> _accessControlServiceMock = new();

        [Fact]
        public async Task GrantAccess_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1 };
            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(request.RoleId, request.DoorId)).ReturnsAsync(true);
            var controller = new AccessControlController(_accessControlServiceMock.Object);

            // Act
            var result = await controller.GrantAccess(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Access granted successfully.", okResult.Value);
        }

        [Fact]
        public async Task RevokeAccess_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1 };
            _accessControlServiceMock.Setup(service => service.RevokeAccessAsync(request.RoleId, request.DoorId)).ReturnsAsync(true);
            var controller = new AccessControlController(_accessControlServiceMock.Object);

            // Act
            var result = await controller.RevokeAccess(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Access revoked successfully.", okResult.Value);

        }

        [Fact]
        public async Task CheckAccess_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new AccessCheck { UserId = 1, DoorId = 1, TagCode = "123123123123", IsRemoteAccessRequested = false };
            _accessControlServiceMock.Setup(service => service.CanAccessDoorAsync(request.UserId, request.DoorId, request.TagCode, request.IsRemoteAccessRequested)).ReturnsAsync(true);
            var controller = new AccessControlController(_accessControlServiceMock.Object);

            // Act
            var result = await controller.CheckAccess(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CheckAccessResult>(okResult.Value);
            Assert.True(returnValue.HasAccess);
        }
        [Fact]
        public async Task GrantAccess_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1 };
            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(request.RoleId, request.DoorId)).ReturnsAsync(false);
            var controller = new AccessControlController(_accessControlServiceMock.Object);

            // Act
            var result = await controller.GrantAccess(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to grant access.", badRequestResult.Value);
        }

        [Fact]
        public async Task GrantAccess_NullRequest_ReturnsBadRequestResult()
        {
            // Arrange
            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
            var controller = new AccessControlController(_accessControlServiceMock.Object);

            // Act
            var result = await controller.GrantAccess(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

    }
}
