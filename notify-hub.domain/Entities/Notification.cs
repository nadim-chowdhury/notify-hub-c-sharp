namespace notify_hub.domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public Guid RecipientId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ReadAtUtc { get; set; }
}