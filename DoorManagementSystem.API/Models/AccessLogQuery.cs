using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.API.Models
{
    public class AccessLogQuery
    {
        public int? UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsSuccess { get; set; }
    }
}
