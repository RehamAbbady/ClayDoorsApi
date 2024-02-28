using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.Application.DTOs
{
    public class AccessRequestDto
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int DoorId { get; set; }
    }

}
