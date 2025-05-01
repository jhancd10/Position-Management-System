using Microsoft.EntityFrameworkCore;
using PositionManagement.Application.Interfaces.Repositories;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;
using PositionManagement.Shared.Exceptions;

namespace PositionManagement.Application.Services
{
    /// <summary>
    /// Service class for managing recruiters. Provides methods to perform CRUD operations
    /// and additional functionalities such as retrieving recruiters with their associated positions.
    /// </summary>
    public class RecruiterService(
        IBaseRepository<Recruiter, Guid> recruiterRepository) : IRecruiterService
    {
        private readonly IBaseRepository<Recruiter, Guid> _recruiterRepository = recruiterRepository;

        /// <summary>
        /// Validates whether a recruiter with the specified cellphone or email already exists in the repository.
        /// Optionally excludes a recruiter by their unique identifier during the validation.
        /// </summary>
        /// <param name="cellphone">The cellphone number to validate.</param>
        /// <param name="email">The email address to validate.</param>
        /// <param name="excludeId">An optional unique identifier to exclude from the validation.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether a conflict exists.</returns>
        private async Task<bool> Validator(string cellphone, string email, Guid? excludeId = null)
            => await _recruiterRepository.Query()
               .AnyAsync(r =>
                    (r.Cellphone == cellphone || r.Email == email) &&
                    (excludeId == null || r.Id != excludeId)
               );

        /// <summary>
        /// Retrieves all recruiters. Optionally includes their associated positions.
        /// </summary>
        /// <param name="includePositions">Indicates whether to include associated positions.</param>
        /// <returns>A list of recruiters.</returns>
        public async Task<List<Recruiter>> GetAllAsync(bool includePositions = true)
        {
            // Start querying the recruiter repository.
            var query = _recruiterRepository.Query();

            // Include positions if the flag is set to true.
            if (includePositions) query = query.Include(r => r.Positions);

            // Execute the query and return the result as a list.
            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves a recruiter by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the recruiter.</param>
        /// <returns>The recruiter if found.</returns>
        /// <exception cref="NotFoundException">Thrown if the recruiter is not found.</exception>
        public async Task<Recruiter> GetByIdAsync(Guid id)
        {
            // Attempt to retrieve the recruiter by ID.
            var recruiters = await _recruiterRepository.GetByIdAsync(id);

            // Throw an exception if the recruiter is not found.
            return recruiters ?? throw new NotFoundException($"Recruiter with Id '{id}' not found.");
        }

        /// <summary>
        /// Creates a new recruiter.
        /// </summary>
        /// <param name="recruiter">The recruiter to create.</param>
        /// <returns>The created recruiter.</returns>
        /// <exception cref="ConflictException">Thrown if a recruiter with the same cellphone or email already exists.</exception>
        public async Task<Recruiter> CreateAsync(Recruiter recruiter)
        {
            /* Check if a recruiter with the same cellphone or email already exists and
             * Throw an exception if a conflict is detected. */

            if (await Validator(recruiter.Cellphone, recruiter.Email))
                throw new ConflictException("A recruiter with the same cellphone or email already exists.");

            // Add the new recruiter to the repository.
            return await _recruiterRepository.AddAsync(recruiter);
        }

        /// <summary>
        /// Updates an existing recruiter.
        /// </summary>
        /// <param name="recruiter">The recruiter with updated information.</param>
        /// <returns>The updated recruiter.</returns>
        /// <exception cref="ConflictException">Thrown if another recruiter with the same cellphone or email exists.</exception>
        public async Task<Recruiter> UpdateAsync(Recruiter recruiter)
        {
            /* Check if another recruiter with the same cellphone or email exists and
             * Throw an exception if a conflict is detected. */

            if (await Validator(recruiter.Cellphone, recruiter.Email, recruiter.Id))
                throw new ConflictException("Another recruiter with the same cellphone or email exists.");

            // Retrieve the existing recruiter from the repository.
            var recruiterDb = await GetByIdAsync(recruiter.Id);

            // Update the recruiter's properties.
            recruiterDb.Name = recruiter.Name;
            recruiterDb.Cellphone = recruiter.Cellphone;
            recruiterDb.Email = recruiter.Email;

            // Save the updated recruiter to the repository.
            return await _recruiterRepository.UpdateAsync(recruiterDb);
        }

        /// <summary>
        /// Deletes a recruiter by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the recruiter to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(Guid id)
        {
            // Retrieve the recruiter to delete from the repository.
            var recruiterDb = await GetByIdAsync(id);

            // Delete the recruiter from the repository.
            await _recruiterRepository.DeleteAsync(recruiterDb);
        }
    }
}
