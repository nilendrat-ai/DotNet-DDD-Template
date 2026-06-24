using DotNet.DDD.Template.Domain.Common;

namespace DotNet.DDD.Template.Domain.Aggregates.Orders;

public record OrderCreatedEvent(Guid OrderId, string CustomerId) : IDomainEvent;
public record OrderSubmittedEvent(Guid OrderId) : IDomainEvent;
