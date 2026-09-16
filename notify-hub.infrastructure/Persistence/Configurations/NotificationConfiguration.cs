using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using notify_hub.domain.Entities;

namespace notify_hub.infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(200).IsRequired();

        builder.Property(x => x.Message).HasMaxLength(2000).IsRequired();

        builder.Property(x => x.RecipientId).IsRequired();

        builder.Property(x => x.CreatedAtUtc).IsRequired();

        builder.HasIndex(x => x.RecipientId);

        builder.HasIndex(x => new { x.RecipientId, x.IsRead });
    }
}
