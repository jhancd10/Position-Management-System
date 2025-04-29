using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PositionManagement.Infrastructure.Filters
{
    /// <summary>
    /// Represents an operation filter that adds an API Key header to Swagger documentation.
    /// </summary>
    public class ApiKeyHeader : IOperationFilter
    {
        private readonly string _header;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyHeader"/> class with the specified header name.
        /// </summary>
        /// <param name="header">The name of the API Key header.</param>
        public ApiKeyHeader(string header) => _header = header;

        /// <summary>
        /// Applies the API Key header to the Swagger operation.
        /// </summary>
        /// <param name="operation">The Swagger operation to modify.</param>
        /// <param name="context">The context for the operation filter.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= [];

            if (!string.IsNullOrEmpty(_header))
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = _header,
                    In = ParameterLocation.Header,
                    Required = true,
                    Description = "API Key required",
                    Schema = new OpenApiSchema() { Type = "string" }
                });
        }
    }
}
