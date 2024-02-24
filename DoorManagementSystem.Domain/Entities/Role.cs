using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{

    public class Role
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role_name")]
        [Required]
        public string RoleName { get; set; }
        [Column("can_access_logs")]
        public bool CanAccessLogs { get; set; }
        public IList<UserRole> UserRoles { get; set; }
        public IList<RoleDoor> RoleDoors { get; set; }

    }
}
