using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{
    public class DoorAccess
    {
        [Key]
        [Column("access_role_id")]
        public int AccessRoleId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("door_id")]
        public int DoorId { get; set; }
        [ForeignKey("user_id")]

        public User User { get; set; }
        [ForeignKey("door_id")]

        public Door Door { get; set; }

    }
}
