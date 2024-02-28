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

        public async Task<DoorLogs> InsertDoorLogs(DateTime? startDate, DateTime? endDate, int? userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Doors> GetByIdAsync(int id)
        {
            return await _context.Doors.FindAsync(id);
        }

        public async Task<DoorLogs> GetDoorLogs(DateTime? startDate, DateTime? endDate, int? userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Doors>> GetDoors()
        {
            return await _context.Doors.ToListAsync();

        }
    }
}
