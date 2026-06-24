using DotNet.DDD.Template.Infrastructure.Persistence.Repositories;
using MediatR;

namespace DotNet.DDD.Template.Application.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly OrderRepository _orders;

    public GetOrderByIdQueryHandler(OrderRepository orders) => _orders = orders;

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null) return null;

        return new OrderDto(
            order.Id,
            order.CustomerId,
            order.Status.ToString(),
            order.TotalAmount.Amount,
            order.TotalAmount.Currency,
            order.CreatedAt,
            order.Lines.Select(l => new OrderLineDto(
                l.Id,
                l.ProductId,
                l.Quantity,
                l.UnitPrice.Amount,
                l.LineTotal.Amount)).ToList());
    }
}
