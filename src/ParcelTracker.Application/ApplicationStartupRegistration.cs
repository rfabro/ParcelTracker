using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Application.Features.Notifications;
using ParcelTracker.Application.Features.Notifications.Services;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application;

public static class ApplicationStartupRegistration
{
    public static IServiceCollection AddFeatures(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Notifications

        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IAsyncRepository<NotificationEntity>, NotificationRepository>();

        #endregion

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}