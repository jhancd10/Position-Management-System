using PositionManagement.Domain.Interfaces;
using System.Linq.Expressions;

namespace PositionManagement.Application.Interfaces.Repositories
{
    /// <summary>
    /// Defines a generic repository interface for managing entities of type <typeparamref name="T"/> with an identifier of type <typeparamref name="TId"/>.
    /// Provides methods for querying, retrieving, adding, updating, and deleting entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public interface IBaseRepository<T, TId> where T : class, IBaseEntity<TId>
    {
        /// <summary>
        /// Retrieves an <see cref="IQueryable{T}"/> for querying the entities.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> for querying the entities.</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Retrieves an entity by its identifier, including specified related entities.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="includes">Expressions specifying related entities to include.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity.</returns>
        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Finds a single entity that matches the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition to match the entity.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the added entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated entity.</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(T entity);
    }
}
