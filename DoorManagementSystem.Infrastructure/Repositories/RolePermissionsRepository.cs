using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoorManagementSystem.Infrastructure.Repositories
{
    public class RolePermissionsRepository : IRolePermissionsRepository
    {
        private readonly DoorManagementContext _context;

        public RolePermissionsRepository(DoorManagementContext context)
        {
            _context = context;
        }

        public async Task<IList<RolePermission>> GetRolePermissionsByRoleIdAsync(int roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .ToListAsync();
        }
        public async Task<KeyValuePair<bool, string>> GrantPermissionToRoleAsync(int roleId, int permissionId)
        {
            bool exists = await _context.RolePermissions
                                        .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (!exists)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };
                _context.RolePermissions.Add(rolePermission);
                await _context.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, "role permission added");
            }

            return new KeyValuePair<bool, string>(false, "role permission already exists");
        }

        public async Task<KeyValuePair<bool, string>> RevokePermissionFromRoleAsync(int roleId, int permissionId)
        {
            var rolePermission = await _context.RolePermissions
                                                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, "role permission deleted");

            }
            return new KeyValuePair<bool, string>(false, "role permission does not exist");
        }

    }

}
