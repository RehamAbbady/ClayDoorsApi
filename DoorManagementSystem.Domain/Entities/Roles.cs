using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Column("admin_role")]
        public bool AdminRole { get; set; }

        public IList<UserRoles> UserRoles { get; set; }
        public IList<RoleDoorAccess> RoleDoors { get; set; }

    }
}
