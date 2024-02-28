using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("email")]
        public string Email { get; set; }
        [Column("pin_hash")]
        public string PinHash { get; set; }
        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        public string LastName { get; set; }



        //navigation properties
        public IList<UserRole> UserRoles { get; set; }
        public IList<UserTag> UserTags { get; set; }
        public IList<DoorLog> DoorLogs { get; set; }

    }
}
