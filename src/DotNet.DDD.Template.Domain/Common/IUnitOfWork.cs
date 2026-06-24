namespace DotNet.DDD.Template.Domain.Common;

/// <summary>
/// Unit of Work — wraps a single transaction boundary. Implemented by AppDbContext.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
