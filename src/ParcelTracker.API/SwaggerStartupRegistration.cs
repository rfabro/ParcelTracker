using Microsoft.OpenApi.Models;

namespace ParcelTracker.API;

public static class SwaggerStartupRegistration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSwaggerGen((options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Parcel Tracker", Version = "v1" });
            options.AddSecurityDefinition("User API Token", new OpenApiSecurityScheme()
            {
                Description = "An API token generated against a specific user",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = "token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                [new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "User API Token",
                        Type = ReferenceType.SecurityScheme
                    }
                }] = Array.Empty<string>()
            });
            options.EnableAnnotations();
        }));
        return services;
    }
}