using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IDoorLogsRepository
    {
        Task LogAccessAttemptAsync(DoorLog doorLog);
        Task<List<DoorLog>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null);


    }
}
