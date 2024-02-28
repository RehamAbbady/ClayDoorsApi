namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IAccessControlService
    {
        Task<bool> GrantAccessAsync(int roleId, int doorId);
        Task<bool> RevokeAccessAsync(int roleId, int doorId);
        Task<bool> CanAccessDoorAsync(int userId, int doorId, string tagCode = null, bool isRemoteAccessRequested = false);
    }
}
