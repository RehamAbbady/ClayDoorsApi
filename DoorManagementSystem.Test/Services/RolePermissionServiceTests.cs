using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Domain.Entities;
using DoorManagementSystem.Domain.Enums;
using Moq;

namespace DoorManagementSystem.Test.Services
{
    public class RolePermissionServiceTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock = new();
        private readonly Mock<IRolePermissionsRepository> _rolePermissionsRepositoryMock = new();
        private readonly Mock<IRolesRepository> _rolesRepositoryMock = new();

        [Fact]
        public async Task HasPermissionForDoorAsync_UserHasPermission_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var doorId = 1;
            var permission = Permissions.OpenDoor;
            var role = new List<Role> { new Role { RoleId = 1 } };
            var rolePermissions = new List<RolePermission> { new RolePermission { Permission = new Permission { Name = permission.ToString() }, IsTemporary = false } };

            _usersRepositoryMock.Setup(repo => repo.GetUserRolesAsync(userId)).ReturnsAsync(role);
            _rolePermissionsRepositoryMock.Setup(repo => repo.GetRolePermissionsByRoleIdAsync(role[0].RoleId)).ReturnsAsync(rolePermissions);
            _rolesRepositoryMock.Setup(repo => repo.CheckAccessAsync(role[0].RoleId, doorId)).ReturnsAsync(true);

            var service = new RolePermissionService(_usersRepositoryMock.Object, _rolePermissionsRepositoryMock.Object, _rolesRepositoryMock.Object);

            // Act
            var result = await service.HasPermissionForDoorAsync(userId, doorId, permission);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task HasPermissionForDoorAsync_UserHasNoPermission_ReturnsFalse()
        {
            // Arrange
            var userId = 1;
            var doorId = 1;
            var permission = Permissions.OpenDoor;
            var role = new List<Role> { new Role { RoleId = 1 } };
            var rolePermissions = new List<RolePermission> { new RolePermission { Permission = new Permission { Name = "DifferentPermission" }, IsTemporary = false } };

            _usersRepositoryMock.Setup(repo => repo.GetUserRolesAsync(userId)).ReturnsAsync(role);
            _rolePermissionsRepositoryMock.Setup(repo => repo.GetRolePermissionsByRoleIdAsync(role[0].RoleId)).ReturnsAsync(rolePermissions);
            _rolesRepositoryMock.Setup(repo => repo.CheckAccessAsync(role[0].RoleId, doorId)).ReturnsAsync(false);

            var service = new RolePermissionService(_usersRepositoryMock.Object, _rolePermissionsRepositoryMock.Object, _rolesRepositoryMock.Object);

            // Act
            var result = await service.HasPermissionForDoorAsync(userId, doorId, permission);

            // Assert
            Assert.False(result);
        }
    }
}
