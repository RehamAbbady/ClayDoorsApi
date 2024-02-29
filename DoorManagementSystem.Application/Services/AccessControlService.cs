using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Domain.Entities;
using DoorManagementSystem.Domain.Enums;
using System.Security;
using System.Security.Claims;

namespace DoorManagementSystem.Application.Services
{
    public class AccessControlService : IAccessControlService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDoorsRepository _doorsRepository;
        private readonly IDoorLogsRepository _doorLogsRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly IRolesRepository _rolesRepository;

        private readonly IRolePermissionService _rolePermissionService;

        public AccessControlService(IUsersRepository usersRepository, IDoorsRepository doorsRepository,
            IDoorLogsRepository doorLogsRepository, IRolePermissionsRepository rolePermissionsRepository, IRolePermissionService rolePermissionService,
            IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _doorsRepository = doorsRepository;
            _doorLogsRepository = doorLogsRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _rolePermissionService = rolePermissionService;
            _rolesRepository = rolesRepository;
        }



        public async Task<KeyValuePair<bool, string>> GrantAccessAsync(int doorId, int roleId, Permissions permissions, int? userId = null)
        {
            if (userId != null)
            {
                return await _usersRepository.AddRoleToUserAsync(userId.Value, roleId);

            }

            var roleDoorResult = await _rolesRepository.GrantAccessAsync(roleId, doorId);
            if (roleDoorResult.Key==false)
            {
                return roleDoorResult;
            }


            var rolePermissionResult = await _rolePermissionsRepository.GrantPermissionToRoleAsync(roleId, (int)permissions);
            if (rolePermissionResult.Key == false)
            {
                return rolePermissionResult;
            }
            return new KeyValuePair<bool, string>(true, "access added successfully");
        }


        public async Task<KeyValuePair<bool, string>> RevokeAccessAsync(int doorId, int roleId, Permissions permissions, int? userId = null)
        {
            if (userId != null)
            {
                return await _usersRepository.RemoveRoleFromUserAsync(userId.Value, roleId);
            }
            else
            {
                int permissionId = (int)(permissions);

                var permissionRevoked = await _rolePermissionsRepository.RevokePermissionFromRoleAsync(roleId, permissionId);
                if (!permissionRevoked.Key)
                {
                    return permissionRevoked;
                }

                var accessRevoked = await _rolesRepository.RevokeAccessFromDoorAsync(roleId, doorId);
                if (!accessRevoked.Key)
                {
                    return accessRevoked;
                }
                return new KeyValuePair<bool, string>(true, "access revoked successfully");
            }

        }

        public async Task<bool> CanOpenDoorAsync(int userId, int doorId, string tagCode = null, bool isRemoteAccessRequested = false)
        {
            var door = await _doorsRepository.GetByIdAsync(doorId);
            if (door == null || (isRemoteAccessRequested && !door.RemoteAccessEnabled))
                return false;

            if (!string.IsNullOrEmpty(tagCode) && !await _usersRepository.IsValidTagAsync(userId, tagCode))
                return false;

            var hasPermission = await _rolePermissionService.HasPermissionForDoorAsync(userId, doorId, Permissions.OpenDoor);

            await LogAccessAttempt(userId, doorId, hasPermission, isRemoteAccessRequested);
            return hasPermission;
        }
        public async Task<bool> AuthorizeRequestUserPermissionAsync(ClaimsPrincipal claimsPrnicipal, int doorId, Permissions permissions)
        {
            var claims = claimsPrnicipal.Claims;
            var requestserId = claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            bool requestUserUasAccess = await _rolePermissionService.HasPermissionForDoorAsync(int.Parse(requestserId), doorId, permissions);
            return requestUserUasAccess;
        }
        private async Task LogAccessAttempt(int userId, int doorId, bool success, bool isRemoteAccessRequested)
        {
            await _doorLogsRepository.LogAccessAttemptAsync(new DoorLog
            {
                UserID = userId,
                DoorID = doorId,
                AccessDateTime = DateTime.UtcNow,
                Success = success,
                IsRemoteAccessRequested = isRemoteAccessRequested
            });
        }

     
    }


}

