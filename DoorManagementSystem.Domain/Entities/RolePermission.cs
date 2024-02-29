using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("role_permission")]
    public class RolePermission
    {
        [Column("role_id")]
        public int RoleId { get; set; }


        [Column("permission_id")]
        public int PermissionId { get; set; }

        [Column("start_time")]
        public DateTime? StartTime { get; set; }

        [Column("end_time")]
        public DateTime? EndTime { get; set; }

        [Column("is_temporary")]
        public bool IsTemporary { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }

    }

}
