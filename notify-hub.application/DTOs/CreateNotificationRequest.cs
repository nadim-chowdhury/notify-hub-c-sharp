namespace notify_hub.application.DTOs;

public record CreateNotificationRequest(Guid RecipientId, string Title, string Message);
