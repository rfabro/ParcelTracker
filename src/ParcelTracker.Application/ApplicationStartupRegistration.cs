using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Application.Features.Configuration.Services;
using ParcelTracker.Application.Features.Notification.Services;
using ParcelTracker.Application.Features.Notification;
using ParcelTracker.Application.Features.Notifications.Services;
using ParcelTracker.Application.Features.Rules.Services;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application;

public static class ApplicationStartupRegistration
{
    public static IServiceCollection AddFeatures(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Notifications

        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IRuleService, RuleService>();

        #endregion

        #region Rules

        services.AddTransient<IAsyncRepository<NotificationEntity>, NotificationRepository>();
        services.AddTransient<IAsyncRepository<RuleEntity>, RuleRepository>();

        #endregion

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}