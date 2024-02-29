using DoorManagementSystem.Domain.Enums;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IRolePermissionService
    {
        Task<bool> HasPermissionForDoorAsync(int userId, int doorId, Permissions permissions);

    }
}
