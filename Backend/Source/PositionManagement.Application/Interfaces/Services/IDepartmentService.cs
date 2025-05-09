using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Interfaces.Services
{
    /// <summary>
    /// Provides operations for managing departments and their related positions
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// Retrieves all departments, optionally including their positions
        /// </summary>
        /// <param name="includePositions">Indicates whether to include positions in the result</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>A list of departments</returns>
        Task<List<Department>> GetAllAsync(bool includePositions = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a department by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the department</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>The department with the specified ID</returns>
        Task<Department> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new department
        /// </summary>
        /// <param name="department">The department entity to create</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>The created department</returns>
        Task<Department> CreateAsync(Department department, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing department
        /// </summary>
        /// <param name="department">The department entity with updated information</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>The updated department</returns>
        Task<Department> UpdateAsync(Department department, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a department by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
