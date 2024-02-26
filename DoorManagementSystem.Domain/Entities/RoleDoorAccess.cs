using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("role_door")]
    public class RoleDoorAccess
    {
        [Key]
        [Column("role_door_id")]

        public int RoleDoorId { get; set; }

        [Required]
        [Column("role_id")]

        public int RoleId { get; set; }

        [Required]
        [Column("door_id")]
        public int DoorId { get; set; }

        public Roles Role { get; set; }
        public Doors Door { get; set; }
    }
}
