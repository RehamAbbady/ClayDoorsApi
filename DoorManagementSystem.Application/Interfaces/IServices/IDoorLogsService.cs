using DoorManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IDoorLogsService
    {
        Task<List<DoorLogs>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null);

    }
}
