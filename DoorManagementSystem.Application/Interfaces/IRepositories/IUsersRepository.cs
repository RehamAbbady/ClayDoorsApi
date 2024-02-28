using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(int id);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<List<UserTags>> GetUserTagsAsync(int userId);
        Task<bool> IsValidTagAsync(int userId, string tagCode);
        Task<IEnumerable<Roles>> GetUserRolesAsync(int userId);
        Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
        Task<bool> AddRoleToUserAsync(int userId, int roleId);
        Task<bool> IsUserAdminForDoorAsync(int userId, int doorId);



    }
}
