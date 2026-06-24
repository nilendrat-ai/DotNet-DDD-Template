namespace DotNet.DDD.Template.Domain.Common;

/// <summary>
/// Aggregate Root — the consistency boundary. Only the root is accessible via IRepository.
/// </summary>
public abstract class AggregateRoot : Entity
{
    public int Version { get; protected set; }
}
