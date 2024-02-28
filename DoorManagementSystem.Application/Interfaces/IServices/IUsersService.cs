using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);

        Task<UserDto?> GetUserDetailsByEmailAsync(string email);

        Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
        Task<bool> AddRoleToUserAsync(int userId, int roleId);
        Task<bool> IsUserAdminForDoorAsync(int userId, int doorId);

    }
}
