using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IRolesRepository
    {
        Task<KeyValuePair<bool, string>> RevokeAccessFromDoorAsync(int roleId, int doorId);
        Task<KeyValuePair<bool, string>> GrantAccessAsync(int roleId, int doorId);
        Task<bool> CheckAccessAsync(int roleId, int doorId);
    }
}