using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
