using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoorManagementSystem.Infrastructure.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DoorManagementContext _context;

        public RolesRepository(DoorManagementContext context)
        {
            _context = context;
        }

        public async Task<KeyValuePair<bool, string>> RevokeAccessFromDoorAsync(int roleId, int doorId)
        {
            var roleDoorAccess = await _context.RoleDoors
                                                .FirstOrDefaultAsync(rda => rda.RoleId == roleId && rda.DoorId == doorId);
            if (roleDoorAccess != null)
            {
                _context.RoleDoors.Remove(roleDoorAccess);
                await _context.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, "role door removed");
            }
            return new KeyValuePair<bool, string>(false, "role door does not exist");
        }
        public async Task<KeyValuePair<bool, string>> GrantAccessAsync(int roleId, int doorId)
        {
            bool exists = await _context.RoleDoors
                                  .AnyAsync(rda => rda.RoleId == roleId && rda.DoorId == doorId);

            if (!exists)
            {
                var roleDoorAccess = new RoleDoor
                {
                    RoleId = roleId,
                    DoorId = doorId
                };
                _context.RoleDoors.Add(roleDoorAccess);
                await _context.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, "user role added");
            }

            return new KeyValuePair<bool, string>(false, "user role already exists");
        }

        public async Task<bool> CheckAccessAsync(int roleId, int doorId)
        {
            return await _context.RoleDoors.AnyAsync(rda => rda.RoleId == roleId && rda.DoorId == doorId);
        }

    }
}
