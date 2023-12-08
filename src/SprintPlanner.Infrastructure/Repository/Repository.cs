namespace SprintPlanner.Infrastructure.Repository;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private readonly SprintPlannerDbContext _sprintPlannerDbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(SprintPlannerDbContext sprintPlannerDbContext)
    {
        ArgumentNullException.ThrowIfNull(sprintPlannerDbContext);

        _sprintPlannerDbContext = sprintPlannerDbContext;
        _dbSet = sprintPlannerDbContext.Set<TEntity>();
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken)
    {
        return _dbSet.AsNoTracking().AnyAsync(cancellationToken);
    }

    public IQueryable<TEntity> AsQueryable() => _dbSet;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _sprintPlannerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _sprintPlannerDbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        _dbSet.UpdateRange(entities);
        await _sprintPlannerDbContext.SaveChangesAsync(cancellationToken);
    }
}