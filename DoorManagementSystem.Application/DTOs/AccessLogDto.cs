﻿namespace DoorManagementSystem.Application.DTOs
{
    public class AccessLogDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DoorId { get; set; }
        public DateTime AccessTime { get; set; }
        public bool IsSuccess { get; set; }
    }

}
