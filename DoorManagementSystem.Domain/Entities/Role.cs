using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("roles")]
    public class Role
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role_name")]
        [Required]
        public string RoleName { get; set; }

        public IList<UserRole> UserRoles { get; set; }
        public IList<RoleDoor> RoleDoors { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }


    }
}
