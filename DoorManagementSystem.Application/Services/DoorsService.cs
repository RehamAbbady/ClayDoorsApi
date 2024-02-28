using DoorManagementSystem.Application.Interfaces.IRepositories;

namespace DoorManagementSystem.Application.Services
{
    public class DoorsService
    {
        private readonly IDoorsRepository _doorRepository;

        public DoorsService(IDoorsRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

    }
}
