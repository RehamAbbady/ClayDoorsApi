using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.DTOs
{
    public class AccessCheckDto
    {
        public int UserId { get; set; }
        public int DoorId { get; set; }
        public string TagCode { get; set; }
        public bool IsRemoteAccessRequested { get; set; }
    }
}
