using DoorManagementSystem.Domain.Entities;
using DoorManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IRolePermissionService
    {
        Task<bool> HasPermissionForDoorAsync(int userId, int doorId, Permissions permissions);

    }
}
