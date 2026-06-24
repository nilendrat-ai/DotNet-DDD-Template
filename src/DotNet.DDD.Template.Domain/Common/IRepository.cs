namespace DotNet.DDD.Template.Domain.Common;

/// <summary>
/// Generic repository contract. Implemented in Infrastructure — never referenced there from Domain.
/// </summary>
public interface IRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T aggregate, CancellationToken cancellationToken = default);
    void Update(T aggregate);
    void Remove(T aggregate);
}
