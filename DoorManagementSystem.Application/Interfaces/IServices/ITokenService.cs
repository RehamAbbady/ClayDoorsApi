using DoorManagementSystem.Application.DTOs;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserDto user);
    }
}
