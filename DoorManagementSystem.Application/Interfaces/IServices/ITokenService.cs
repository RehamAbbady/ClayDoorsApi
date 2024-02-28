using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateJwtToken(Users user);
    }
}
