using notify_hub.application.DTOs;

namespace notify_hub.application.Interfaces;

public interface INotificationService
{
    Task<NotificationResponse> CreateAsync(
        CreateNotificationRequest request,
        CancellationToken cancellationToken = default
    );
    Task<NotificationResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyList<NotificationResponse>> GetByRecipientIdAsync(
        Guid recipientId,
        CancellationToken cancellationToken = default
    );
    Task<bool> MarkAsReadAsync(Guid id, CancellationToken cancellationToken = default);
}
