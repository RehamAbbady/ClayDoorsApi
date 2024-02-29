using DoorManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.Application.DTOs
{
    public class AccessRequestDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "RoleId must be greater than 0")]

        public int RoleId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DoorId must be greater than 0")]

        public int DoorId { get; set; }
        public int? UserId { get; set; }
        public Permissions RequestedPermission { get; set; }
    }

}
