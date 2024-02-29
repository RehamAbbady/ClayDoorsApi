using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("user_roles")]
    public class UserRole
    {

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("role_id")]
        [Required]
        public int RoleId { get; set; }

        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
