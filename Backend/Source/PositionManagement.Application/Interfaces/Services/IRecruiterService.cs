using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Interfaces.Services
{
    /// <summary>
    /// Provides methods for managing recruiters and their associated data
    /// </summary>
    public interface IRecruiterService
    {
        /// <summary>
        /// Retrieves all recruiters, optionally including their associated positions
        /// </summary>
        /// <param name="includePositions">Indicates whether to include associated positions</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
        /// <returns>A list of recruiters</returns>
        Task<List<Recruiter>> GetAllAsync(bool includePositions = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a recruiter by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the recruiter</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
        /// <returns>The recruiter with the specified identifier</returns>
        Task<Recruiter> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new recruiter
        /// </summary>
        /// <param name="recruiter">The recruiter entity to create</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
        /// <returns>The created recruiter</returns>
        Task<Recruiter> CreateAsync(Recruiter recruiter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing recruiter
        /// </summary>
        /// <param name="recruiter">The recruiter entity with updated data</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
        /// <returns>The updated recruiter</returns>
        Task<Recruiter> UpdateAsync(Recruiter recruiter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a recruiter by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the recruiter to delete</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
