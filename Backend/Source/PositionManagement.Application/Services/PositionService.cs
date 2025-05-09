using Microsoft.EntityFrameworkCore;
using PositionManagement.Application.Interfaces.Repositories;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;
using PositionManagement.Shared.Exceptions;

namespace PositionManagement.Application.Services
{
    /// <summary>  
    /// Provides services for managing positions, including CRUD operations and validation to ensure data integrity  
    /// </summary>  
    public class PositionService(
        IBaseRepository<Position, Guid> positionRepository) : IPositionService
    {
        private readonly IBaseRepository<Position, Guid> _positionRepository = positionRepository;

        /// <summary>  
        /// Checks if a position with the same details already exists, optionally excluding a specific position by ID  
        /// </summary>  
        /// <param name="position">The position to validate</param>  
        /// <param name="excludeId">Optional ID to exclude from validation</param>  
        /// <returns>True if a duplicate exists, otherwise false</returns>  
        private async Task<bool> Validator(
            Position position,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default
        ) =>
            // Query the repository to check for any matching positions  
            await _positionRepository.Query()
              .AnyAsync(p =>
                   (p.RecruiterId == position.RecruiterId &&
                    p.Title == position.Title &&
                    p.Description == position.Description &&
                    p.Location == position.Location &&
                    p.Status == position.Status &&
                    p.DepartmentId == position.DepartmentId &&
                    p.Budget == position.Budget &&
                    p.ClosingDate == position.ClosingDate) &&
                   (excludeId == null || p.Id != excludeId),
              cancellationToken);

        /// <summary>  
        /// Retrieves all positions, optionally including related details  
        /// </summary>  
        /// <param name="includeDetails">Whether to include related details</param>  
        /// <returns>A list of positions</returns>  
        public async Task<List<Position>> GetAllAsync(
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            // Query the repository for positions  
            var query = _positionRepository.Query();

            // Include related details if specified  
            if (includeDetails)
                query = query.Include(p => p.Recruiter)
                             .Include(p => p.Department);

            // Return the list of positions  
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>  
        /// Retrieves a position by its ID, including related details  
        /// </summary>  
        /// <param name="id">The ID of the position</param>  
        /// <returns>The position if found</returns>  
        public async Task<Position> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Retrieve the position by ID, including related details  
            var position = await _positionRepository.GetByIdAsync(
                id: id,
                cancellationToken: cancellationToken,
                p => p.Recruiter,
                p => p.Department
            );

            // Throw an exception if the position is not found  
            return position ?? throw new NotFoundException($"Position with Id '{id}' not found");
        }

        /// <summary>  
        /// Creates a new position after validating for duplicates  
        /// </summary>  
        /// <param name="position">The position to create</param>  
        /// <returns>The created position</returns>  
        public async Task<Position> CreateAsync(Position position, CancellationToken cancellationToken = default)
        {
            // Check if a position with the same details already exists per recruiter  
            if (await Validator(position, cancellationToken: cancellationToken))
                throw new ConflictException("The recruiter already has a position with identical details");

            // Add the new position to the repository  
            return await _positionRepository.AddAsync(position, cancellationToken: cancellationToken);
        }

        /// <summary>  
        /// Updates an existing position after validating for duplicates  
        /// </summary>  
        /// <param name="position">The position to update</param>  
        /// <returns>The updated position</returns>  
        public async Task<Position> UpdateAsync(Position position, CancellationToken cancellationToken = default)
        {
            // Check if another position with the same details already exists per recruiter  
            if (await Validator(position, position.Id, cancellationToken))
                throw new ConflictException("The recruiter already has another position with identical details");

            // Retrieve the existing position from the repository  
            var positionDb = await GetByIdAsync(position.Id, cancellationToken);

            // Update the position details  
            positionDb.Title = position.Title;
            positionDb.Description = position.Description;
            positionDb.Location = position.Location;
            positionDb.Status = position.Status;
            positionDb.RecruiterId = position.RecruiterId;
            positionDb.DepartmentId = position.DepartmentId;
            positionDb.Budget = position.Budget;
            positionDb.ClosingDate = position.ClosingDate;

            // Save the updated position to the repository  
            return await _positionRepository.UpdateAsync(positionDb, cancellationToken);
        }

        /// <summary>  
        /// Deletes a position by its ID  
        /// </summary>  
        /// <param name="id">The ID of the position to delete</param>  
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Retrieve the position by ID  
            var positionDb = await GetByIdAsync(id, cancellationToken);

            // Delete the position from the repository  
            await _positionRepository.DeleteAsync(positionDb, cancellationToken);
        }
    }
}
