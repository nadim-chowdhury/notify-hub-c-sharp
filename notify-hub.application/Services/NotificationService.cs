using notify_hub.application.DTOs;
using notify_hub.application.Interfaces;
using notify_hub.domain.Entities;

namespace notify_hub.application.Services;

public class NotificationService(INotificationRepository repository) : INotificationService
{
    public async Task<NotificationResponse> CreateAsync(
        CreateNotificationRequest request,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.RecipientId == Guid.Empty)
        {
            throw new ArgumentException("RecipientId cannot be empty.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(request));
        }

        if (request.Title.Length > 200)
        {
            throw new ArgumentException("Title cannot exceed 200 characters.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ArgumentException("Message cannot be empty.", nameof(request));
        }

        if (request.Message.Length > 2000)
        {
            throw new ArgumentException("Message cannot exceed 2000 characters.", nameof(request));
        }

        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            RecipientId = request.RecipientId,
            Title = request.Title.Trim(),
            Message = request.Message.Trim(),
            IsRead = false,
            CreatedAtUtc = DateTime.UtcNow,
            ReadAtUtc = null,
        };

        await repository.AddAsync(notification, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return NotificationResponse.FromEntity(notification);
    }

    public async Task<NotificationResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var notification = await repository.GetByIdAsync(id, cancellationToken);
        return notification is null ? null : NotificationResponse.FromEntity(notification);
    }

    public async Task<IReadOnlyList<NotificationResponse>> GetByRecipientIdAsync(
        Guid recipientId,
        CancellationToken cancellationToken = default
    )
    {
        var notifications = await repository.GetByRecipientIdAsync(recipientId, cancellationToken);
        return notifications.Select(NotificationResponse.FromEntity).ToList();
    }

    public async Task<bool> MarkAsReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var notification = await repository.GetByIdAsync(id, cancellationToken);
        if (notification is null)
        {
            return false;
        }

        if (!notification.IsRead)
        {
            notification.IsRead = true;
            notification.ReadAtUtc = DateTime.UtcNow;
            await repository.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}
