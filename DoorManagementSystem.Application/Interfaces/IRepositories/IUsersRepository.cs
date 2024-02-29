using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IUsersRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> IsValidTagAsync(int userId, string tagCode);
        Task<IEnumerable<Role>> GetUserRolesAsync(int userId);
        Task<KeyValuePair<bool, string>> RemoveRoleFromUserAsync(int userId, int roleId);
        Task<KeyValuePair<bool, string>> AddRoleToUserAsync(int userId, int roleId);



    }
}
