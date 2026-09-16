using Microsoft.EntityFrameworkCore;
using notify_hub.application.Interfaces;
using notify_hub.domain.Entities;
using notify_hub.infrastructure.Persistence;

namespace notify_hub.infrastructure.Repositories;

public class NotificationRepository(ApplicationDbContext dbContext) : INotificationRepository
{
    public async Task<Notification?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return await dbContext.Notifications.FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<Notification>> GetByRecipientIdAsync(
        Guid recipientId,
        CancellationToken cancellationToken = default
    )
    {
        return await dbContext
            .Notifications.Where(x => x.RecipientId == recipientId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken = default
    )
    {
        await dbContext.Notifications.AddAsync(notification, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
