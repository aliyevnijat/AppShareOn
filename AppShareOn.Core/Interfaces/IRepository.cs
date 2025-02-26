using System.Linq.Expressions;

namespace AppShareOn.Core.Interfaces;

/// <summary>
/// Generic repository for all entities
/// </summary>
/// <typeparam name="TEntity">Type of entity for the repository.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Gets list of entries for a given entity.
    /// </summary>
    /// <param name="filter">Expression to filter the results.</param>
    /// <returns>Collection for the given entity.</returns>
    Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);

    /// <summary>
    /// Gets an entity by identifier. Can include properties if includeProperties 
    /// parameter is provided as string array.
    /// </summary>
    /// <param name="id">Unique identifier for the entity.</param>
    /// <param name="includeProperties">
    /// List of properties to include as string array. Nested include properties should be 
    /// dot separated property names. Ex. ["Tenant", "Profiles.Platform"]
    /// </param>
    /// <returns>Single entity.</returns>
    Task<TEntity?> GetByIdAsync(Guid id, string[]? includeProperties = null);

    /// <summary>
    /// Adds an entity to the repository.
    /// </summary>
    /// <param name="entity">Entity to be added.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Adds a collection of entities to the repository.
    /// </summary>
    /// <param name="entities">Collection of entities.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates the entity in the repository.
    /// </summary>
    /// <param name="entity">Entity to be updated.</param>
    /// <returns></returns>
    void Update(TEntity entity);

    /// <summary>
    /// Updates a collection of entities in the repository.
    /// </summary>
    /// <param name="entities">Collection of entities.</param>
    /// <returns></returns>
    void UpdateRange(IEnumerable<TEntity> entities);
}