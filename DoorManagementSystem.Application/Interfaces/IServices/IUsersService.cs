using DoorManagementSystem.Application.DTOs;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto?> GetUserDetailsByEmailAsync(string email);


    }
}
