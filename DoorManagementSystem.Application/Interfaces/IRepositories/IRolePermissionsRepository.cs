using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IRolePermissionsRepository
    {
        Task<IList<RolePermission>> GetRolePermissionsByRoleIdAsync(int roleId);
        Task<KeyValuePair<bool, string>> GrantPermissionToRoleAsync(int roleId, int permissionId);
        Task<KeyValuePair<bool, string>> RevokePermissionFromRoleAsync(int roleId, int permissionId);
    }

}
