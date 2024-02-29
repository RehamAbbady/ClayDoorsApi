using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Domain.Entities;
using DoorManagementSystem.Domain.Enums;
using Moq;
using System.Security.Claims;

namespace DoorManagementSystem.Test.Services
{
    public class AccessControlServiceTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock = new();
        private readonly Mock<IDoorsRepository> _doorsRepositoryMock = new();
        private readonly Mock<IDoorLogsRepository> _doorLogsRepositoryMock = new();
        private readonly Mock<IRolePermissionsRepository> _rolePermissionsRepositoryMock = new();
        private readonly Mock<IRolesRepository> _rolesRepositoryMock = new();
        private readonly Mock<IRolePermissionService> _rolePermissionServiceMock = new();

        [Fact]
        public async Task GrantAccessAsync_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var doorId = 1;
            var roleId = 1;
            var userId = 1;
            var permissions = Permissions.OpenDoor;

            _usersRepositoryMock.Setup(repo => repo.AddRoleToUserAsync(userId, roleId)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));
            _rolesRepositoryMock.Setup(repo => repo.GrantAccessAsync(roleId, doorId)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));
            _rolePermissionsRepositoryMock.Setup(repo => repo.GrantPermissionToRoleAsync(roleId, (int)permissions)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));

            var service = new AccessControlService(_usersRepositoryMock.Object, _doorsRepositoryMock.Object, _doorLogsRepositoryMock.Object, _rolePermissionsRepositoryMock.Object, _rolePermissionServiceMock.Object, _rolesRepositoryMock.Object);

            // Act
            var result = await service.GrantAccessAsync(doorId, roleId, permissions, userId);

            // Assert
            Assert.True(result.Key);
        }

        [Fact]
        public async Task RevokeAccessAsync_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var doorId = 1;
            var roleId = 1;
            var userId = 1;
            var permissions = Permissions.OpenDoor;

            _usersRepositoryMock.Setup(repo => repo.RemoveRoleFromUserAsync(userId, roleId)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));
            _rolesRepositoryMock.Setup(repo => repo.RevokeAccessFromDoorAsync(roleId, doorId)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));
            _rolePermissionsRepositoryMock.Setup(repo => repo.RevokePermissionFromRoleAsync(roleId, (int)permissions)).ReturnsAsync(new KeyValuePair<bool, string>(true, "success"));

            var service = new AccessControlService(_usersRepositoryMock.Object, _doorsRepositoryMock.Object, _doorLogsRepositoryMock.Object, _rolePermissionsRepositoryMock.Object, _rolePermissionServiceMock.Object, _rolesRepositoryMock.Object);

            // Act
            var result = await service.RevokeAccessAsync(doorId, roleId, permissions, userId);

            // Assert
            Assert.True(result.Key);
        }

        [Fact]
        public async Task CanOpenDoorAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var doorId = 1;
            var tagCode = "123123123123";
            var isRemote = false;

            _doorsRepositoryMock.Setup(repo => repo.GetByIdAsync(doorId)).ReturnsAsync(new Door { DoorId = doorId, RemoteAccessEnabled = true });
            _usersRepositoryMock.Setup(repo => repo.IsValidTagAsync(userId, tagCode)).ReturnsAsync(true);
            _rolePermissionServiceMock.Setup(service => service.HasPermissionForDoorAsync(userId, doorId, Permissions.OpenDoor)).ReturnsAsync(true);

            var service = new AccessControlService(_usersRepositoryMock.Object, _doorsRepositoryMock.Object, _doorLogsRepositoryMock.Object, _rolePermissionsRepositoryMock.Object, _rolePermissionServiceMock.Object, _rolesRepositoryMock.Object);

            // Act
            var result = await service.CanOpenDoorAsync(userId, doorId, tagCode, isRemote);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AuthorizeRequestUserPermissionAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var doorId = 1;
            var permissions = Permissions.OpenDoor;
            var claims = new List<Claim> { new Claim("user_id", "1") };
            var identity = new ClaimsIdentity(claims, "mock");
            var principal = new ClaimsPrincipal(identity);

            _rolePermissionServiceMock.Setup(service => service.HasPermissionForDoorAsync(int.Parse(claims[0].Value), doorId, permissions)).ReturnsAsync(true);

            var service = new AccessControlService(_usersRepositoryMock.Object, _doorsRepositoryMock.Object, _doorLogsRepositoryMock.Object, _rolePermissionsRepositoryMock.Object, _rolePermissionServiceMock.Object, _rolesRepositoryMock.Object);

            // Act
            var result = await service.AuthorizeRequestUserPermissionAsync(principal, doorId, permissions);

            // Assert
            Assert.True(result);
        }
    }
}
