using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("user_roles")]
    public class UserRole
    {
        [Key]
        [Column("user_role_id")]
        public int UserRoleId { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("role_id")]
        [Required]
        public int RoleId { get; set; }
        [Column("admin_role")]
        public bool AdminRole { get; set; }
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
