using Microsoft.Extensions.DependencyInjection;
using notify_hub.application.Interfaces;
using notify_hub.application.Services;

namespace notify_hub.application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
        return services;
    }
}
