using AutoMapper;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Services
{

    public class DoorLogsService : IDoorLogsService
    {
        private readonly IDoorLogsRepository _doorLogsRepository;
        private readonly IMapper _mapper;

        public DoorLogsService(IDoorLogsRepository doorLogRepository, IMapper mapper)
        {
            _doorLogsRepository = doorLogRepository;
            _mapper = mapper;
        }

        public async Task<List<AccessLogDto>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null)
        {
            var logs = await _doorLogsRepository.GetAccessLogsAsync(userId, doorId, startDate, endDate, isSuccess);
            return _mapper.Map<List<AccessLogDto>>(logs);
        }
    }
}
