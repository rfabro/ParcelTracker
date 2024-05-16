using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Application.Features.Emails.Options;
using ParcelTracker.Application.Features.Emails.Services;
using ParcelTracker.Application.Features.Notification.Services;
using ParcelTracker.Application.Features.Notification;
using ParcelTracker.Application.Features.Notifications.Services;
using ParcelTracker.Application.Features.Rules.Services;
using ParcelTracker.Core.Email;
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

        #region Rules

        services.AddTransient<IRuleService, RuleService>();
        services.AddTransient<IAsyncRepository<RuleEntity>, RuleRepository>();

        #endregion

        #region Email

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ISendGridService, SendGridService>();
        services.Configure<SendGridOptions>(options =>
        {
            options.SendGridApiKey = configuration["SendGridApiKey"];
        });
        #endregion

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}