
namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IAccessControlRepository
    {
        Task<bool> GrantAccessAsync(int roleId, int doorId);
        Task<bool> RevokeAccessAsync(int roleId, int doorId);
        Task<bool> CheckAccessAsync(int roleId, int doorId);
    }
}
