using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Infrastructure.Repositories
{
    public class AccessControlRepository : IAccessControlRepository
    {
        private readonly DoorManagementContext _context;

        public AccessControlRepository(DoorManagementContext context)
        {
            _context = context;
        }
        public async Task<bool> GrantAccessAsync(int roleId, int doorId)
        {
            if (await CheckAccessAsync(roleId, doorId))
            {
                return false; 
            }

            _context.RoleDoorAccess.Add(new RoleDoorAccess { RoleId = roleId, DoorId = doorId });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RevokeAccessAsync(int roleId, int doorId)
        {
            var access = await _context.RoleDoorAccess
                .FirstOrDefaultAsync(rda => rda.RoleId == roleId && rda.DoorId == doorId);
            if (access == null)
            {
                return false; 
            }

            _context.RoleDoorAccess.Remove(access);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckAccessAsync(int roleId, int doorId)
        {
            return await _context.RoleDoorAccess.AnyAsync(rda => rda.RoleId == roleId && rda.DoorId == doorId);
        }

    }

}
