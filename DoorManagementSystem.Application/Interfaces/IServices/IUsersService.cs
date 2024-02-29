using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto?> GetUserDetailsByEmailAsync(string email);


    }
}
