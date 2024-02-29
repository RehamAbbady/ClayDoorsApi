using DoorManagementSystem.Domain.Enums;
using System.Security.Claims;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IAccessControlService
    {
        Task<KeyValuePair<bool, string>> GrantAccessAsync(int doorId, int roleId, Permissions permissions, int? userId = null);
        Task<KeyValuePair<bool, string>> RevokeAccessAsync(int doorId, int roleId, Permissions permissions, int? userId = null);
        Task<bool> CanOpenDoorAsync(int userId, int doorId, string tagCode = null, bool isRemoteAccessRequested = false);
        Task<bool> AuthorizeRequestUserPermissionAsync(ClaimsPrincipal claimsPrnicipal, int doorId, Permissions permissions);

    }
}
