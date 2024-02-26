using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IDoorsRepository
    {
        Task<IEnumerable<Doors>> GetDoors();
        Task<Doors> GetByIdAsync(int id);
        Task<DoorLogs> GetDoorLogs(DateTime? startDate, DateTime? endDate, int? userId);

        Task<DoorLogs> InsertDoorLogs(DateTime? startDate, DateTime? endDate, int? userId);
    }
}
