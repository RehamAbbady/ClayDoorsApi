﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("door_logs")]
    public class DoorLog
    {
        [Key]
        [Column("log_id")]
        public int LogId { get; set; }
        [Required]
        [Column("access_date_time")]
        public DateTime AccessDateTime { get; set; }

        [Required]
        [Column("success")]
        public bool Success { get; set; }

        [Required]
        [Column("user_id")]
        public int UserID { get; set; }

        [Required]
        [Column("door_id")]
        public int DoorID { get; set; }

        [Required]
        [Column("is_remote_access_requested")]
        public bool IsRemoteAccessRequested { get; set; }
        public User User { get; set; }
        public Door Door { get; set; }

    }
}
