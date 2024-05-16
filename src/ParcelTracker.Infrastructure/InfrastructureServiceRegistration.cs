using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelTracker.Infrastructure.Contexts;

namespace ParcelTracker.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationsContext>();
        services.AddDbContext<RulesContext>();
        return services;
    }
}