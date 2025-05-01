using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Application.Interfaces.Repositories;
using PositionManagement.Infrastructure.Repositories;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for registering repository services in the dependency injection container.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Registers the base repository services in the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the repository services will be added.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            // Registers the IBaseRepository interface with its implementation BaseRepository as a scoped service
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            // Returns the updated service collection
            return services;
        }
    }
}
