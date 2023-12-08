namespace SprintPlanner.Infrastructure.Repository;

public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<bool> AnyAsync(CancellationToken cancellationToken);
    IQueryable<TEntity> AsQueryable();
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
}