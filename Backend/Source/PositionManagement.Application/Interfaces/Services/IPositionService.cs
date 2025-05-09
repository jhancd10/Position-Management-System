using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Interfaces.Services
{
    /// <summary>
    /// Provides methods to manage positions, including retrieval, creation, updating, and deletion
    /// </summary>
    public interface IPositionService
    {
        /// <summary>
        /// Retrieves all positions, optionally including detailed information
        /// </summary>
        /// <param name="includeDetails">Indicates whether to include detailed information</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>A list of positions</returns>
        Task<List<Position>> GetAllAsync(bool includeDetails = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a position by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the position</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>The position with the specified identifier</returns>
        Task<Position> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new position
        /// </summary>
        /// <param name="position">The position to create</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>The created position</returns>
        Task<Position> CreateAsync(Position position, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing position
        /// </summary>
        /// <param name="position">The position to update</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>The updated position</returns>
        Task<Position> UpdateAsync(Position position, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a position by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the position to delete</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
