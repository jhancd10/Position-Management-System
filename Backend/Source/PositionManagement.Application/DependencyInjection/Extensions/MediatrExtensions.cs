using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Application.Behaviors;
using System.Reflection;

namespace PositionManagement.Application.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods to configure and integrate MediatR behaviors and handlers into the application's dependency injection container.
    /// </summary>
    public static class MediatrExtensions
    {
        /// <summary>
        /// Registers MediatR services, handlers, and pipeline behaviors into the application's service collection.
        /// </summary>
        /// <param name="services">The service collection to which MediatR services will be added.</param>
        /// <returns>The updated service collection with MediatR configured.</returns>
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            // Add MediatR services to the service collection
            services.AddMediatR(cfg =>
            {
                // Register all MediatR handlers and services from the current assembly
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                // Add a custom logging behavior to the MediatR pipeline
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

                // Add a custom timing behavior to the MediatR pipeline
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TimingBehavior<,>));
            });

            // Return the service collection with MediatR configured
            return services;
        }
    }
}
