using DotNet.DDD.Template.Domain.Common;

namespace DotNet.DDD.Template.Domain.Aggregates.Orders;

public class OrderLine : Entity
{
    public string ProductId { get; private set; } = default!;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = default!;
    public Money LineTotal => Money.From(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    private OrderLine() { }

    public OrderLine(string productId, int quantity, Money unitPrice)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
