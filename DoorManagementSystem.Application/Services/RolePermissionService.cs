﻿using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Domain.Enums;

namespace DoorManagementSystem.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {

        private readonly IUsersRepository _usersRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly IRolesRepository _rolesRepository;

        public RolePermissionService(IUsersRepository usersRepository, IRolePermissionsRepository rolePermissionsRepository, IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _rolesRepository = rolesRepository;
        }

        public async Task<bool> HasPermissionForDoorAsync(int userId, int doorId, Permissions permission)
        {
            var userRoles = await _usersRepository.GetUserRolesAsync(userId);
            foreach (var userRole in userRoles)
            {
                var permissions = await _rolePermissionsRepository.GetRolePermissionsByRoleIdAsync(userRole.RoleId);
                var access = await _rolesRepository.CheckAccessAsync(userRole.RoleId, doorId);
                if (permissions.Any(p => p.Permission.Name == permission.ToString() &&
                                         (!p.IsTemporary || (p.StartTime <= DateTime.UtcNow && p.EndTime >= DateTime.UtcNow)) &&
                                         access))
                {
                    return true;
                }

            }
            return false;
        }
    }

}

