using NetPower.Domain.Entities.Common;

namespace NetPower.Domain.Interfaces.Repositories;

/// <summary>
/// Generic repository interface for basic CRUD operations on aggregate roots.
/// </summary>
/// <typeparam name="T">The aggregate root type.</typeparam>
public interface IGenericRepository<T> where T : AuditableEntity, IAggregateRoot
{
    /// <summary>
    /// Gets all entities as an enumerable.
    /// </summary>
    IQueryable<T> GetAll();

    /// <summary>
    /// Gets an entity by its ID.
    /// </summary>
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Saves all changes to the data store.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
