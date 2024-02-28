using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Services
{
    public class AccessControlService : IAccessControlService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDoorsRepository _doorsRepository;
        private readonly IAccessControlRepository _accessControlRepository;
        private readonly IDoorLogsRepository _doorLogsRepository;


        public AccessControlService(IUsersRepository usersRepository, IDoorsRepository doorsRepository, IAccessControlRepository accessControlRepository, IDoorLogsRepository doorLogsRepository)
        {
            _usersRepository = usersRepository;
            _doorsRepository = doorsRepository;
            _accessControlRepository = accessControlRepository;
            _doorLogsRepository = doorLogsRepository;
        }

        public async Task<bool> GrantAccessAsync(int roleId, int doorId)
        {
            var door = await _doorsRepository.GetByIdAsync(doorId);

            if (door == null)
            {
                return false;
            }

            return await _accessControlRepository.GrantAccessAsync(roleId, doorId);
        }

        public async Task<bool> RevokeAccessAsync(int userId, int doorId)
        {
            return await _accessControlRepository.RevokeAccessAsync(userId, doorId);
        }

        public async Task<bool> CanAccessDoorAsync(int userId, int doorId, string tagCode = null, bool isRemoteAccessRequested = false)
        {
            var door = await _doorsRepository.GetByIdAsync(doorId);
            bool accessGranted = false;

            if (door != null && (!isRemoteAccessRequested || (isRemoteAccessRequested && door.RemoteAccessEnabled)))
            {
                if (string.IsNullOrEmpty(tagCode) || await _usersRepository.IsValidTagAsync(userId, tagCode))
                {
                    var userRoles = await _usersRepository.GetUserRolesAsync(userId);
                    foreach (var role in userRoles)
                    {
                        accessGranted = await _accessControlRepository.CheckAccessAsync(role.RoleId, doorId);
                        if (accessGranted)
                        {
                            break;
                        }
                    }
                }
            }

            await _doorLogsRepository.LogAccessAttemptAsync(new DoorLogs
            {
                UserID = userId,
                DoorID = doorId,
                AccessDateTime = DateTime.UtcNow,
                Success = accessGranted,
                IsRemoteAccessRequested = isRemoteAccessRequested
            });

            return accessGranted;
        }
    }
}
