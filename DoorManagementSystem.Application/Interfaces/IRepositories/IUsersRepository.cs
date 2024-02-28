using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> IsValidTagAsync(int userId, string tagCode);
        Task<IEnumerable<Role>> GetUserRolesAsync(int userId);
        Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
        Task<bool> AddRoleToUserAsync(int userId, int roleId);
        Task<bool> IsUserAdminForDoorAsync(int userId, int doorId);



    }
}
