using DoorManagementSystem.Application.DTOs;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IDoorLogsService
    {
        Task<List<AccessLogDto>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null);

    }
}
