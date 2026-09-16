using notify_hub.domain.Entities;

namespace notify_hub.application.DTOs;

public record NotificationResponse(
    Guid Id,
    Guid RecipientId,
    string Title,
    string Message,
    bool IsRead,
    DateTime CreatedAtUtc,
    DateTime? ReadAtUtc
)
{
    public static NotificationResponse FromEntity(Notification notification) =>
        new(
            notification.Id,
            notification.RecipientId,
            notification.Title,
            notification.Message,
            notification.IsRead,
            notification.CreatedAtUtc,
            notification.ReadAtUtc
        );
}
