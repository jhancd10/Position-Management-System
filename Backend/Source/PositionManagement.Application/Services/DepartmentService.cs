using Microsoft.EntityFrameworkCore;
using PositionManagement.Application.Interfaces.Repositories;
using PositionManagement.Application.Interfaces.Services;
using PositionManagement.Domain.Entities;
using PositionManagement.Shared.Exceptions;

namespace PositionManagement.Application.Services
{
    /// <summary>
    /// Service class for managing departments, providing methods for CRUD operations and querying departments with optional related data
    /// </summary>
    public class DepartmentService(
        IBaseRepository<Department, Guid> departmentRepository) : IDepartmentService
    {
        private readonly IBaseRepository<Department, Guid> _departmentRepository = departmentRepository;

        /// <summary>
        /// Validates whether a department with the specified name already exists, optionally excluding a department by its unique identifier
        /// </summary>
        /// <param name="name">The name of the department to validate</param>
        /// <param name="excludeId">The unique identifier of the department to exclude from validation (optional)</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether a department with the specified name exists</returns>
        private async Task<bool> Validator(
            string name,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default
        ) =>
            // Query the repository to check if a department with the same name exists
            await _departmentRepository.Query()
            .AnyAsync(d =>
                (d.Name == name) &&
                (excludeId == null || d.Id != excludeId),
            cancellationToken);

        /// <summary>
        /// Retrieves all departments, optionally including their related positions
        /// </summary>
        /// <param name="includePositions">Indicates whether to include related positions in the result</param>
        /// <returns>A list of departments, optionally with their related positions</returns>
        public async Task<List<Department>> GetAllAsync(
            bool includePositions = true,
            CancellationToken cancellationToken = default)
        {
            // Start querying the department repository
            var query = _departmentRepository.Query();

            // Include related positions if specified
            if (includePositions) query = query.Include(d => d.Positions);

            // Execute the query and return the result as a list
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves a department by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the department</param>
        /// <returns>The department if found; otherwise, throws a NotFoundException</returns>
        public async Task<Department> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Attempt to retrieve the department by its ID
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);

            // Throw an exception if the department is not found
            return department ?? throw new NotFoundException($"Department with Id '{id}' not found");
        }

        /// <summary>
        /// Creates a new department
        /// </summary>
        /// <param name="department">The department to create</param>
        /// <returns>The created department</returns>
        /// <exception cref="ConflictException">Thrown if a department with the same name already exists</exception>
        public async Task<Department> CreateAsync(Department department, CancellationToken cancellationToken = default)
        {
            // Check if a department with the same name already exists
            if (await Validator(department.Name, cancellationToken: cancellationToken))
                throw new ConflictException("A department with the same name already exists");

            // Add the new department to the repository
            return await _departmentRepository.AddAsync(department, cancellationToken);
        }

        /// <summary>
        /// Updates an existing department with new data
        /// </summary>
        /// <param name="department">The updated department data</param>
        /// <returns>The updated department</returns>
        /// <exception cref="ConflictException">Thrown if another department with the same name exists</exception>
        public async Task<Department> UpdateAsync(Department department, CancellationToken cancellationToken = default)
        {
            // Check if another department with the same name exists
            if (await Validator(department.Name, department.Id, cancellationToken))
                throw new ConflictException("Another department with the same name exists");

            // Retrieve the existing department by its ID
            var departmentDb = await GetByIdAsync(department.Id, cancellationToken);

            // Update the department's name
            departmentDb.Name = department.Name;

            // Save the updated department to the repository
            return await _departmentRepository.UpdateAsync(departmentDb, cancellationToken);
        }

        /// <summary>
        /// Deletes a department by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Retrieve the department to delete by its ID
            var departmentDb = await GetByIdAsync(id, cancellationToken);

            // Delete the department from the repository
            await _departmentRepository.DeleteAsync(departmentDb, cancellationToken);
        }
    }
}
