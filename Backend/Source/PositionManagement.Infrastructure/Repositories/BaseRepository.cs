using Microsoft.EntityFrameworkCore;
using PositionManagement.Application.Interfaces.Repositories;
using PositionManagement.Domain.Interfaces;
using PositionManagement.Infrastructure.Data;
using System.Linq.Expressions;

namespace PositionManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Provides a base implementation for repository operations, including querying, adding, updating, and deleting entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public class BaseRepository<T, TId>(
        PositionManagementDbContext context)
        : IBaseRepository<T, TId> where T : class, IBaseEntity<TId>
    {
        // Database context instance
        private readonly PositionManagementDbContext _context = context;

        // DbSet for the entity type
        private readonly DbSet<T> _dbSet = context.Set<T>();

        /// <summary>
        /// Returns a queryable collection of entities without tracking changes.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> of entities.</returns>
        public IQueryable<T> Query() => _dbSet.AsNoTracking();

        /// <summary>
        /// Retrieves an entity by its identifier, including specified related entities.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="includes">Expressions specifying related entities to include.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            // Start with a queryable collection
            var query = Query();

            // Include related entities as specified
            foreach (var include in includes)
                query = query.Include(include);

            // Find the entity by its identifier
            return await query.FirstOrDefaultAsync(entity => entity.EntityId.Equals(id));
        }

        /// <summary>
        /// Finds a single entity that matches the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition to match.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate)
            // Execute the query with the predicate
            => await Query().FirstOrDefaultAsync(predicate);

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        public async Task<T> AddAsync(T entity)
        {
            // Add the entity to the DbSet
            var entry = await _dbSet.AddAsync(entity);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the added entity
            return entry.Entity;
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        public async Task<T> UpdateAsync(T entity)
        {
            // Mark the entity as updated
            _dbSet.Update(entity);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated entity
            return entity;
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public async Task DeleteAsync(T entity)
        {
            // Remove the entity from the DbSet.
            _dbSet.Remove(entity);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
