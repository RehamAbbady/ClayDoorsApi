using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoorManagementSystem.Infrastructure.Repositories
{
    public class DoorsRepository : IDoorsRepository
    {
        private readonly DoorManagementContext _context;
        public DoorsRepository(DoorManagementContext context)
        {
            _context = context;
        }
        public async Task<Door> GetByIdAsync(int id)
        {
            return await _context.Doors.FindAsync(id);
        }

    }
}
