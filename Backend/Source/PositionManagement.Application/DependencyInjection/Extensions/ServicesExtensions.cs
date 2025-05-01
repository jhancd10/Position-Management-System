using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Application.Services;

namespace PositionManagement.Application.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for registering application services in the dependency injection container
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Registers application services in the dependency injection container
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <returns>The updated IServiceCollection</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register IDepartmentService with its implementation DepartmentService
            services.AddScoped<IDepartmentService, DepartmentService>();

            // Register IRecruiterService with its implementation RecruiterService
            services.AddScoped<IRecruiterService, RecruiterService>();

            // Register IPositionService with its implementation PositionService
            services.AddScoped<IPositionService, PositionService>();

            // Return the updated IServiceCollection
            return services;
        }
    }
}
