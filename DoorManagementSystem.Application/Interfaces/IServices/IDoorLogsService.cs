﻿using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IDoorLogsService
    {
        Task<List<DoorLogs>> GetAccessLogsAsync(int? userId = null, int? doorId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isSuccess = null);

    }
}
