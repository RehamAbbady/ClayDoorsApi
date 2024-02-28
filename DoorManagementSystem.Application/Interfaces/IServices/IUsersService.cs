using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IUsersService
    {
        public Task<IEnumerable<UserDto>> GetAllUsersAsync();
        public Task<UserDto> GetUserByIdAsync(int id);
        Task<Users?> GetUserDetailsByEmailAsync(string email);

        public Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
        public Task<bool> AddRoleToUserAsync(int userId, int roleId);
        public Task<bool> IsUserAdminForDoorAsync(int userId, int doorId);

    }
}
