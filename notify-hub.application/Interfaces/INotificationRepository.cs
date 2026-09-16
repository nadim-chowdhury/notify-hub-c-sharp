using notify_hub.domain.Entities;

namespace notify_hub.application.Interfaces;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Notification>> GetByRecipientIdAsync(
        Guid recipientId,
        CancellationToken cancellationToken = default
    );
    Task AddAsync(Notification notification, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
