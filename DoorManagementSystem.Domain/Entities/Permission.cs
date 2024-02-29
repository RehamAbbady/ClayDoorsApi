using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("permissions")]
    public class Permission
    {
        [Key]
        [Column("permission_id")]
        public int PermissionId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }

        public IList<RolePermission> RolePermissions { get; set; }
    }
}
