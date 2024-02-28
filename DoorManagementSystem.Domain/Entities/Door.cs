using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("doors")]
    public class Door
    {
        [Key]
        [Column("door_id")]
        public int DoorId { get; set; }

        [Required]
        [Column("location")]
        public string Location { get; set; }

        [Required]
        [Column("door_name")]
        public string DoorName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("remote_acccess_enabled")]
        public bool RemoteAccessEnabled { get; set; }

        public IList<RoleDoorAccess> RoleDoors { get; set; }
        public IList<DoorLog> DoorLogs { get; set; }


    }
}
