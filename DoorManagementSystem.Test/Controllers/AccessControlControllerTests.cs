using DoorManagementSystem.API.Controllers;
using DoorManagementSystem.API.Models;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace DoorManagementSystem.Test.Controllers
{
    public class AccessControlControllerTests
    {
        private readonly Mock<IAccessControlService> _accessControlServiceMock = new();


        [Fact]
        public async Task GrantAccess_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1, UserId = 1, RequestedPermission = Permissions.OpenDoor };
            var mockClaims = new List<Claim>
            {
                new Claim("user_id", "2")
            };
            var mockIdentity = new ClaimsIdentity(mockClaims, "mock");
            var mockPrincipal = new ClaimsPrincipal(mockIdentity);
            _accessControlServiceMock.Setup(service => service.AuthorizeRequestUserPermissionAsync(mockPrincipal, request.DoorId, Permissions.OpenDoor)).ReturnsAsync(true);

            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(request.DoorId, request.RoleId, Permissions.OpenDoor, request.UserId)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));
            var controller = new AccessControlController(_accessControlServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockPrincipal }
            };


            // Act
            var result = await controller.GrantAccess(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RevokeAccess_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1, UserId = 1, RequestedPermission = Permissions.OpenDoor };
            var mockClaims = new List<Claim>
        {
            new Claim("user_id", "2")
        };
            var mockIdentity = new ClaimsIdentity(mockClaims, "mock");
            var mockPrincipal = new ClaimsPrincipal(mockIdentity);
            _accessControlServiceMock.Setup(service => service.AuthorizeRequestUserPermissionAsync(mockPrincipal, request.DoorId, Permissions.OpenDoor)).ReturnsAsync(true);

            _accessControlServiceMock.Setup(service => service.RevokeAccessAsync(request.DoorId, request.RoleId, Permissions.OpenDoor, request.UserId)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));
            var controller = new AccessControlController(_accessControlServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockPrincipal }
            };

            // Act
            var result = await controller.RevokeAccess(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);


        }

        [Fact]
        public async Task CheckAccess_ValidRequest_ReturnsOkResult()
        {
            // Arrange

            _accessControlServiceMock.Setup(service => service.CanOpenDoorAsync(1, 1, "123123123123", false)).ReturnsAsync(true);
            var controller = new AccessControlController(_accessControlServiceMock.Object);

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
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1, UserId = 1, RequestedPermission = Permissions.OpenDoor };
            var mockClaims = new List<Claim>
        {
            new Claim("user_id", "2")
        };
            var mockIdentity = new ClaimsIdentity(mockClaims, "mock");
            var mockPrincipal = new ClaimsPrincipal(mockIdentity);

            _accessControlServiceMock.Setup(service => service.AuthorizeRequestUserPermissionAsync(mockPrincipal, request.DoorId, Permissions.OpenDoor)).ReturnsAsync(true);

            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(request.DoorId, request.RoleId, Permissions.OpenDoor, request.UserId)).ReturnsAsync(new KeyValuePair<bool, string>(false, "fail"));
            var controller = new AccessControlController(_accessControlServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockPrincipal }
            };

            // Act
            var result = await controller.GrantAccess(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GrantAccess_NullRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1, UserId = 1, RequestedPermission = Permissions.OpenDoor }; ;
            var mockClaims = new List<Claim>
        {
            new Claim("user_id", "2")
        };
            var mockIdentity = new ClaimsIdentity(mockClaims, "mock");
            var mockPrincipal = new ClaimsPrincipal(mockIdentity);
            _accessControlServiceMock.Setup(service => service.AuthorizeRequestUserPermissionAsync(mockPrincipal, request.DoorId, Permissions.OpenDoor)).ReturnsAsync(true);

            _accessControlServiceMock.Setup(service => service.GrantAccessAsync(0, 0, 0, null)).ReturnsAsync(new KeyValuePair<bool, string>(false, "fail"));
            var controller = new AccessControlController(_accessControlServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockPrincipal }
            };

            // Act
            var result = await controller.GrantAccess(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task GrantAccess_UnAuthorizedAction_UnAuthorized()
        {
            // Arrange
            var request = new AccessRequestDto { RoleId = 1, DoorId = 1, UserId = 1, RequestedPermission = Permissions.OpenDoor };

            var mockClaims = new List<Claim>
        {
            new Claim("user_id", "2")
        };
            var mockIdentity = new ClaimsIdentity(mockClaims, "mock");
            var mockPrincipal = new ClaimsPrincipal(mockIdentity);
            _accessControlServiceMock.Setup(service => service.AuthorizeRequestUserPermissionAsync(mockPrincipal, request.DoorId, Permissions.OpenDoor)).ReturnsAsync(false);

            var controller = new AccessControlController(_accessControlServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockPrincipal }
            };

            // Act
            var result = await controller.GrantAccess(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);

        }

    }
}
