using Xunit;
using Moq;
using Moq;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.API.Controllers;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using DoorManagementSystem.API.Models;

namespace DoorManagementSystem.Test
{
    public class DoorLogsControllerTests
    {
        private readonly Mock<IDoorLogsService> _doorLogsServiceMock = new();

        [Fact]
        public async Task GetAccessLogs_ValidQuery_ReturnsOkResultWithLogs()
        {
            // Arrange
            var query = new AccessLogQuery { UserId = 1, DoorId = 1 };
            _doorLogsServiceMock.Setup(service => service.GetAccessLogsAsync(query.UserId, query.DoorId, null, null, null)).ReturnsAsync(new List<AccessLogDto>());
            var controller = new DoorLogsController(_doorLogsServiceMock.Object);

            // Act
            var result = await controller.GetAccessLogs(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<AccessLogDto>>(okResult.Value);
        }

        [Fact]
        public async Task GetAccessLogs_InvalidQuery_ReturnsBadRequestResult()
        {
            // Arrange
            var query = new AccessLogQuery { UserId = 1, DoorId = 1 }; // Assume this is invalid
            _doorLogsServiceMock.Setup(service => service.GetAccessLogsAsync(query.UserId, query.DoorId, null, null, null)).ReturnsAsync((List<AccessLogDto>)null);
            var controller = new DoorLogsController(_doorLogsServiceMock.Object);
            controller.ModelState.AddModelError("error", "invalid input"); // Make the model state invalid

            // Act
            var result = await controller.GetAccessLogs(query);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
