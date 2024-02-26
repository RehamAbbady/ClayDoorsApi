using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface ISecurityService
    {
      string HashPin(string pin);
      bool VerifyPin(string providedPin, string storedHash);
    }
}
