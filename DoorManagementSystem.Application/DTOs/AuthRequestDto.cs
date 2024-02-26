using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.DTOs
{
    public class AuthRequestDto
    {
        public string Email { get; set; }
        public string Pin { get; set; }
    }
}
