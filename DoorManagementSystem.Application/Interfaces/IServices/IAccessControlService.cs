using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface IAccessControlService
    {
        Task<bool> GrantAccessAsync(int roleId, int doorId);
        Task<bool> RevokeAccessAsync(int roleId, int doorId);
        Task<bool> CanAccessDoorAsync(int userId, int doorId, string tagCode = null, bool isRemoteAccessRequested = false);
    }
}
