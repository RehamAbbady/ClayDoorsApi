using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("users")]
    public class Users
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
        public IList<UserRoles> UserRoles { get; set; }
        public IList<UserTags> UserTags { get; set; }
        public IList<DoorLogs> DoorLogs { get; set; }
       // public IList<DoorAccess> DoorAccesses { get; set; }

    }
}
