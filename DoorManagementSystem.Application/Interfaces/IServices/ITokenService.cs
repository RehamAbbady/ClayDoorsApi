using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateJwtToken(Users user);
    }
}
