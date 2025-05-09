using PositionManagement.Domain.Interfaces;
using System.Linq.Expressions;

namespace PositionManagement.Application.Interfaces.Repositories
{
    /// <summary>
    /// Provides a generic interface for repository operations, enabling querying, retrieval, addition, updating, and deletion of entities of type <typeparamref name="T"/> with an identifier of type <typeparamref name="TId"/>
    /// </summary>
    /// <typeparam name="T">The type of the entity</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier</typeparam>
    public interface IBaseRepository<T, TId> where T : class, IBaseEntity<TId>
    {
        /// <summary>
        /// Returns an <see cref="IQueryable{T}"/> to perform LINQ queries on the entities
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> for querying the entities</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Asynchronously retrieves an entity by its identifier, including specified related entities
        /// </summary>
        /// <param name="id">The unique identifier of the entity</param>
        /// <param name="includes">Expressions specifying related entities to include</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>A task that represents the asynchronous operation, containing the entity</returns>
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default,
                             params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Asynchronously finds a single entity that matches the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to match the entity</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>A task that represents the asynchronous operation, containing the entity if found; otherwise, null</returns>
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously adds a new entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>A task that represents the asynchronous operation, containing the added entity</returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously updates an existing entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>A task that represents the asynchronous operation, containing the updated entity</returns>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously deletes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
