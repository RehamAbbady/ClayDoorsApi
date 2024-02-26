using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("user_roles")]
    public class UserRoles
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
        public Users User { get; set; } = null!;
        public Roles Role { get; set; } = null!;
    }
}
