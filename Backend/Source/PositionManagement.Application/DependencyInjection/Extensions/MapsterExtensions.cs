using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Application.DTOs;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods to configure and register Mapster mappings and mapper services
    /// </summary>
    public static class MapsterExtensions
    {
        /// <summary>
        /// Adds and configures Mapster mappings and mapper service to the service collection
        /// </summary>
        /// <param name="services">The service collection to which Mapster will be added</param>
        /// <returns>The updated service collection</returns>
        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            // Create a dedicated TypeAdapterConfig
            var config = new TypeAdapterConfig();

            // Prevent circular references between related entities
            config.Default.PreserveReference(true);

            /* Position, Department and Recruiter mappings:
             *   - Ignore null values during mapping.
             *   - Enable two-way mapping to allow mapping in both directions. */

            // Configure mapping between Position and PositionDto
            config.NewConfig<Position, PositionDto>()
                .Map(destination => destination.Department, source => source.Department) // Map Department property
                .Map(destination => destination.Recruiter, source => source.Recruiter) // Map Recruiter property
                .IgnoreNullValues(true)
                .TwoWays();

            // Configure mapping between Department and DepartmentDto
            config.NewConfig<Department, DepartmentDto>()
                .Map(destination => destination.Positions, source => source.Positions) // Map Positions property
                .IgnoreNullValues(true)
                .TwoWays();

            // Configure mapping between Recruiter and RecruiterDto
            config.NewConfig<Recruiter, RecruiterDto>()
                .Map(destination => destination.Positions, source => source.Positions) // Map Positions property
                .IgnoreNullValues(true)
                .TwoWays();

            // Compile configuration for performance
            config.Compile();

            // Register TypeAdapterConfig as a singleton
            services.AddSingleton(config);

            // Register IMapper with ServiceMapper implementation
            services.AddScoped<IMapper, ServiceMapper>();

            // Return the updated service collection
            return services;
        }
    }
}
