using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.DTOs
{
    public class UserDoorAccessDetailsDto
    {
        public int DoorId { get; set; }
        public string DoorName { get; set; }
        public string RoleName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
