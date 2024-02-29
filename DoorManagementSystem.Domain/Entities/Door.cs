using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public IList<RoleDoor> RoleDoors { get; set; }
        public IList<DoorLog> DoorLogs { get; set; }


    }
}
