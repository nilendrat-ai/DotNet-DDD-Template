using DotNet.DDD.Template.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace DotNet.DDD.Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Generic EF Core repository implementation.
/// </summary>
public class EfRepository<T> : IRepository<T> where T : AggregateRoot
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public EfRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task AddAsync(T aggregate, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(aggregate, cancellationToken);

    public void Update(T aggregate)
        => _dbSet.Update(aggregate);

    public void Remove(T aggregate)
        => _dbSet.Remove(aggregate);
}
