using Microsoft.EntityFrameworkCore;
using notify_hub.domain.Entities;

namespace notify_hub.infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options){
    public DbSet<Notification> Notifications => Set<Notification>();

    protector overridevoid OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        )

        base.OnModelCreating(modelBuilder);
    }
}