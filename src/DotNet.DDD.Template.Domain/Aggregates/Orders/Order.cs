using DotNet.DDD.Template.Domain.Common;

namespace DotNet.DDD.Template.Domain.Aggregates.Orders;

/// <summary>
/// Order aggregate root — example DDD aggregate enforcing business invariants.
/// </summary>
public class Order : AggregateRoot
{
    public string CustomerId { get; private set; } = default!;
    public OrderStatus Status { get; private set; }
    public Money TotalAmount { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    private readonly List<OrderLine> _lines = new();
    public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();

    private Order() { } // EF Core

    public static Order Create(string customerId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);

        var order = new Order
        {
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            TotalAmount = Money.Zero,
            CreatedAt = DateTime.UtcNow
        };

        order.RaiseDomainEvent(new OrderCreatedEvent(order.Id, customerId));
        return order;
    }

    public void AddLine(string productId, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot modify a non-pending order.");

        var line = new OrderLine(productId, quantity, unitPrice);
        _lines.Add(line);
        TotalAmount = Money.From(TotalAmount.Amount + line.LineTotal.Amount, TotalAmount.Currency);
    }

    public void Submit()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be submitted.");
        if (!_lines.Any())
            throw new InvalidOperationException("Cannot submit an empty order.");

        Status = OrderStatus.Submitted;
        RaiseDomainEvent(new OrderSubmittedEvent(Id));
    }
}

public enum OrderStatus { Pending, Submitted, Fulfilled, Cancelled }
