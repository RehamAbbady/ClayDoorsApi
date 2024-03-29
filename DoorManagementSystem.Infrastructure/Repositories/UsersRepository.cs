﻿using DoorManagementSystem.Application.Interfaces.IRepositories;
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

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> IsValidTagAsync(int userId, string tagCode)
        {
            return await _context.UserTags
                .AnyAsync(ut => ut.UserId == userId && ut.TagCode == tagCode);
        }
        public async Task<IEnumerable<Role>> GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }
        public async Task<KeyValuePair<bool, string>> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null)
            {
                return new KeyValuePair<bool, string>(false, "user role does not exist");
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
            return new KeyValuePair<bool, string>(true, "user role removed");
        }
        public async Task<KeyValuePair<bool, string>> AddRoleToUserAsync(int userId, int roleId)
        {
            var exists = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (exists)
            {
                return new KeyValuePair<bool, string>(false, "user role does already exist");
            }

            var userRole = new UserRole { UserId = userId, RoleId = roleId };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return new KeyValuePair<bool, string>(true, "user role added");
        }

    }
}
