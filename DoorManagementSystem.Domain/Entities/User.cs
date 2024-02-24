﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("email")]
        public string Email { get; set; }
        //navigation properties
        public IList<UserRole> UserRoles { get; set; }
        public IList<UserTag> UserTags { get; set; }
        public IList<DoorLog> DoorLogs { get; set; }
        public IList<DoorAccess> DoorAccesses { get; set; }

    }
}
