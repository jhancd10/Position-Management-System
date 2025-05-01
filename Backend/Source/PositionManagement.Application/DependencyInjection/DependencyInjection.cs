using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Application.DependencyInjection.Extensions;

namespace PositionManagement.Application.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to configure dependency injection for the application layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures application-specific services and dependencies.
        /// </summary>
        /// <param name="services">The service collection to which dependencies will be added.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            // Adds FluentValidation services to the dependency injection container
            services.AddFluentValidation();

            // Adds MediatR services to the dependency injection container
            services.AddMediatr();

            // Adds Application custom services to the dependency injection container
            services.AddApplicationServices();

            // Returns the updated service collection
            return services;
        }
    }
}
