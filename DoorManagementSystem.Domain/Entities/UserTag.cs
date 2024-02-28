using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("user_tags")]
    public class UserTag
    {
        [Key]
        [Column("tag_id")]
        public int TagId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("tag_code")]
        [StringLength(12, MinimumLength = 12)]
        public string TagCode { get; set; }

        public User User { get; set; }
    }
}
