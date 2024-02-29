using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.Application.DTOs
{
    public class AuthRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Pin { get; set; }
    }
}
