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
    public class DoorLogsControllerTests
    {
        private readonly Mock<IDoorLogsService> _doorLogsServiceMock = new();
        private readonly Mock<IAccessControlService> _accessControlServiceMock = new();


        [Fact]
        public async Task GetAccessLogs_ValidQuery_ReturnsOkResultWithLogs()
        {
            // Arrange
            var query = new AccessLogQuery { UserId = 1 };
            var doorId = 1;
            var mockClaims = new List<Claim>
        {
            new Claim("user_id", "2")
        };
            var mockIdentity = new ClaimsIdentity(mockClaims, "mock");
            var mockPrincipal = new ClaimsPrincipal(mockIdentity);
            _accessControlServiceMock.Setup(service => service.AuthorizeRequestUserPermissionAsync(mockPrincipal, doorId, Permissions.ViewLogs)).ReturnsAsync(true);

            _doorLogsServiceMock.Setup(service => service.GetAccessLogsAsync(query.UserId, doorId, null, null, null)).ReturnsAsync(new List<AccessLogDto>());

            var controller = new DoorLogsController(_doorLogsServiceMock.Object, _accessControlServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockPrincipal }
            };
            // Act
            var result = await controller.GetAccessLogs(doorId, query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<AccessLogDto>>(okResult.Value);
        }

        [Fact]
        public async Task GetAccessLogs_InvalidQuery_ReturnsBadRequestResult()
        {
            // Arrange
            var query = new AccessLogQuery { UserId = 1 }; // Assume this is invalid
            int doorId = 1;
            var mockClaims = new List<Claim>
        {
            new Claim("user_id", "2")
        };
            var mockIdentity = new ClaimsIdentity(mockClaims, "mock");
            var mockPrincipal = new ClaimsPrincipal(mockIdentity);
            _accessControlServiceMock.Setup(service => service.AuthorizeRequestUserPermissionAsync(mockPrincipal, doorId, Permissions.ViewLogs)).ReturnsAsync(true);

            _doorLogsServiceMock.Setup(service => service.GetAccessLogsAsync(query.UserId, doorId, null, null, null)).ReturnsAsync((List<AccessLogDto>)null);

            var controller = new DoorLogsController(_doorLogsServiceMock.Object, _accessControlServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockPrincipal }
            };

            controller.ModelState.AddModelError("error", "invalid input");

            // Act
            var result = await controller.GetAccessLogs(doorId, query);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
