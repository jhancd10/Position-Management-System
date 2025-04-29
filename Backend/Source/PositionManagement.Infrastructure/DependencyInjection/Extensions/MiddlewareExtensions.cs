using Microsoft.AspNetCore.Builder;
using PositionManagement.Infrastructure.Middleware;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring middleware in the application pipeline.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configures the middleware components for the application.
        /// </summary>
        /// <param name="webApp">The web application instance to configure.</param>
        public static void UseMiddleware(WebApplication webApp)
        {
            // Adds the ExceptionHandlingMiddleware to the application's middleware pipeline
            webApp.UseMiddleware<ExceptionHandlingMiddleware>();

            // Adds the ApiKeyMiddleware to the application's middleware pipeline
            webApp.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
