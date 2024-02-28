using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("roles")]
    public class Roles
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role_name")]
        [Required]
        public string RoleName { get; set; }

        public IList<UserRoles> UserRoles { get; set; }
        public IList<RoleDoorAccess> RoleDoors { get; set; }

    }
}
