using DotNet.DDD.Template.Domain.Common;

namespace DotNet.DDD.Template.Domain.Aggregates.Orders;

/// <summary>
/// Money value object — immutable, equality by value.
/// </summary>
public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public static Money Zero => new(0, "AUD");

    private Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
        Amount = amount;
        Currency = currency;
    }

    public static Money From(decimal amount, string currency = "AUD") => new(amount, currency);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Currency} {Amount:F2}";
}
