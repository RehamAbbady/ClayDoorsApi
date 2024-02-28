using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IDoorLogsRepository
    {
        Task LogAccessAttemptAsync(DoorLogs doorLog);
        Task<List<DoorLogs>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null);


    }
}
