using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PositionManagement.Application.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods to configure and integrate FluentValidation services into the application's dependency injection container.
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Registers all FluentValidation validators from the current assembly into the application's service collection.
        /// </summary>
        /// <param name="services">The service collection to which the validators will be added.</param>
        /// <returns>The updated service collection with FluentValidation services registered.</returns>
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            // Register all validators found in the current assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Return the updated service collection
            return services;
        }
    }
}
