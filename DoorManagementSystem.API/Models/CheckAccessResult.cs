namespace DoorManagementSystem.API.Models
{
    public class CheckAccessResult
    {
        public int UserId { get; set; }
        public int DoorId { get; set; }
        public bool HasAccess { get; set; }
    }
}
