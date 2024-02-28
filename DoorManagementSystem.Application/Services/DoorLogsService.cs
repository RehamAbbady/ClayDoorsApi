using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Services
{

    public class DoorLogsService : IDoorLogsService
    {
        private readonly IDoorLogsRepository _doorLogsRepository;
        public DoorLogsService(IDoorLogsRepository doorLogRepository)
        {
            _doorLogsRepository = doorLogRepository;
        }

        public async Task<List<DoorLogs>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null)
        {
            return await _doorLogsRepository.GetAccessLogsAsync(userId, doorId, startDate, endDate, isSuccess);
        }
    }
}
