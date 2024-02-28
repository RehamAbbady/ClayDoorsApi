using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace DoorManagementSystem.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DoorManagementContext _context;

        public UsersRepository(DoorManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
        public async Task<List<UserTags>> GetUserTagsAsync(int userId)
        {
            return await _context.UserTags
                .Where(ut => ut.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> IsValidTagAsync(int userId, string tagCode)
        {
            return await _context.UserTags
                .AnyAsync(ut => ut.UserId == userId && ut.TagCode == tagCode);
        }
        public async Task<IEnumerable<Roles>> GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }
        public async Task<bool> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null)
            {
                return false;
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddRoleToUserAsync(int userId, int roleId)
        {
            var exists = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (exists)
            {
                return false;
            }

            var userRole = new UserRoles { UserId = userId, RoleId = roleId };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsUserAdminForDoorAsync(int userId, int doorId)
        {

            var isAdminForDoor = await _context.UserRoles
                .Join(_context.RoleDoorAccess,
                      userRole => userRole.RoleId,
                      roleDoor => roleDoor.RoleId,
                      (userRole, roleDoor) => new { userRole, roleDoor })
                .Where(x => x.userRole.UserId == userId && x.roleDoor.DoorId == doorId)
                .AnyAsync(x => x.userRole.AdminRole);

            return isAdminForDoor;
        }
    }
}
