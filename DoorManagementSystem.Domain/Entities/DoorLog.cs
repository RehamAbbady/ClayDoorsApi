using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{
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
        [ForeignKey("user_id")]
        public User User { get; set; }
        [ForeignKey("door_id")]

        public Door Door { get; set; }

    }
}
