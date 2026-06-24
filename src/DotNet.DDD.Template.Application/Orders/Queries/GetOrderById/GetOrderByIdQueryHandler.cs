using DotNet.DDD.Template.Domain.Aggregates.Orders;
using DotNet.DDD.Template.Domain.Common;
using MediatR;

namespace DotNet.DDD.Template.Application.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IRepository<Order> _orderRepository;

    public GetOrderByIdQueryHandler(IRepository<Order> orderRepository)
        => _orderRepository = orderRepository;

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
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
