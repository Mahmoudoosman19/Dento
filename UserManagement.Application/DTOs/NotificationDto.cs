namespace UserManagement.Application.DTOs
{
    public class NotificationDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public bool IsSeen { get; set; } = false;
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
