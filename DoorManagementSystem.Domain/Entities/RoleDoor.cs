﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("role_door")]
    public class RoleDoor
    {

        [Required]
        [Column("role_id")]

        public int RoleId { get; set; }

        [Required]
        [Column("door_id")]
        public int DoorId { get; set; }

        public Role Role { get; set; }
        public Door Door { get; set; }
    }
}
