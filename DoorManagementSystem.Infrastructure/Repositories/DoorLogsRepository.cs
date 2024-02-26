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
    public class DoorLogsRepository : IDoorLogsRepository
    {
        private readonly DoorManagementContext _context;
        public DoorLogsRepository(DoorManagementContext context)
        {
            _context = context;
        }
        public async Task LogAccessAttemptAsync(DoorLogs doorLog)
        {
            _context.DoorLogs.Add(doorLog);
            await _context.SaveChangesAsync();
        }
        public async Task<List<DoorLogs>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null)
        {
            var query = _context.DoorLogs.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(log => log.UserID == userId.Value);
            }
            if (doorId.HasValue)
            {
                query = query.Where(log => log.DoorID == doorId.Value);
            }
            if (startDate.HasValue)
            {
                query = query.Where(log => log.AccessDateTime >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(log => log.AccessDateTime <= endDate.Value);
            }
            if (isSuccess.HasValue)
            {
                query = query.Where(log => log.Success == isSuccess.Value);
            }
            return await query.ToListAsync();

        }
    }
}
