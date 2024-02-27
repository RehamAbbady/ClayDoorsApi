using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.API.Models
{
    public class AccessLogQuery
    {
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0")]
        public int? UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Door ID must be greater than 0")]
        public int? DoorId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsSuccess { get; set; }
    }
}
