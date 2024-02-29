using Xunit;
using Moq;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.API.Controllers;
using DoorManagementSystem.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoorManagementSystem.API.Models;
using DoorManagementSystem.Domain.Enums;

namespace DoorManagementSystem.Test
{
    public class AccessControlControllerTests
    {
        private readonly Mock<IAccessControlService> _accessControlServiceMock = new();
        private readonly Mock<IRolePermissionService> _rolePermissionServiceMock = new();


        [Fact]
        public async Task GrantAccess_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1 ,UserId=1};
            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(request.DoorId, request.RoleId, Permissions.OpenDoor, request.UserId)).ReturnsAsync(new KeyValuePair<bool, string>(true,"success"));
            var controller = new AccessControlController(_accessControlServiceMock.Object, _rolePermissionServiceMock.Object);

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
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1, UserId = 1 };
            _accessControlServiceMock.Setup(service => service.RevokeAccessAsync(request.DoorId, request.RoleId, Permissions.OpenDoor, request.UserId)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));
            var controller = new AccessControlController(_accessControlServiceMock.Object, _rolePermissionServiceMock.Object);

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
            
            _accessControlServiceMock.Setup(service => service.CanOpenDoorAsync(1, 1, "123123123123", false)).ReturnsAsync(true);
            var controller = new AccessControlController(_accessControlServiceMock.Object, _rolePermissionServiceMock.Object);

            // Act
            var result = await controller.CheckAccess(1, 1, "123123123123", false);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CheckAccessResult>(okResult.Value);
            Assert.True(returnValue.HasAccess);
        }
        [Fact]
        public async Task GrantAccess_InvalidRequest_ReturnsBadRequestResult()
        {
      
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1, UserId = 1 };
            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(request.DoorId, request.RoleId, Permissions.OpenDoor, request.UserId)).ReturnsAsync(new KeyValuePair<bool, string>(false, "fail"));
            var controller = new AccessControlController(_accessControlServiceMock.Object, _rolePermissionServiceMock.Object);

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
            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(0,0,0,null)).ReturnsAsync(new KeyValuePair<bool, string>(false, "fail"));
            var controller = new AccessControlController(_accessControlServiceMock.Object, _rolePermissionServiceMock.Object);

            // Act
            var result = await controller.GrantAccess(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

    }
}
