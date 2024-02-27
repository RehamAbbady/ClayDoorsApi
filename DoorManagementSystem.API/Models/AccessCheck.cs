using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.DTOs
{
    public class AccessCheck
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DoorId must be greater than 0")]
        public int DoorId { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "TagCode must be between 12 characters")]
        public string TagCode { get; set; }

        public bool IsRemoteAccessRequested { get; set; }
    }
}
