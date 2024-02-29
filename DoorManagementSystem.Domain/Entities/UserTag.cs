using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorManagementSystem.Domain.Entities
{
    [Table("user_tags")]
    public class UserTag
    {
        [Column("tag_id")]
        public int TagId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("tag_code")]
        [StringLength(12, MinimumLength = 12)]
        public string TagCode { get; set; }

        public User User { get; set; }
    }
}
