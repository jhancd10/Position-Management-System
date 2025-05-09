using PositionManagement.Application.DependencyInjection;
using PositionManagement.Infrastructure.DependencyInjection;
using PositionManagement.Infrastructure.DependencyInjection.Extensions;
using PositionManagement.Shared.DependencyInjection;

/// <summary>
/// Entry point for the PositionManagement API application.
/// Configures services and middleware for the application.
/// </summary>

var app = DefaultWebApplication.Create(
    args: args,
    webAppBuilder: builder =>
    {
        // Configure services for the application
        builder.Services
            // Add infrastructure dependencies
            .AddInfrastructureDI(builder.Configuration)
            // Add application-level dependencies
            .AddApplicationDI();
    }
);

DefaultWebApplication.Run(
    webApp: app,
    enableSwaggerDocumentation: true,
    configureMiddleware: webApp =>
    {
        // Configure middleware for the application
        MiddlewareExtensions.UseMiddleware(webApp);
    },
    configureSeedDatabase: webApp =>
    {
        // Seed the database with initial data
        DbContextExtensions.SeedDatabase(webApp);
    }
);
