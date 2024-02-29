namespace DoorManagementSystem.Application.DTOs
{
    public class AccessLogDto
    {
        public int LogId { get; set; }
        public DateTime AccessDateTime { get; set; }
        public bool Success { get; set; }
        public int UserID { get; set; }

        public int DoorID { get; set; }
        public bool IsRemoteAccessRequested { get; set; }
    }

}
