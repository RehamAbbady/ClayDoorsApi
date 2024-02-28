using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Interfaces.IRepositories
{
    public interface IDoorsRepository
    {
        Task<Door> GetByIdAsync(int id);
    }
}
