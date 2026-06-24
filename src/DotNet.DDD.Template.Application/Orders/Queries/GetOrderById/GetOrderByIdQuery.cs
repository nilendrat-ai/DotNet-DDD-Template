using MediatR;

namespace DotNet.DDD.Template.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto?>;

public record OrderDto(
    Guid Id,
    string CustomerId,
    string Status,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAt,
    IReadOnlyList<OrderLineDto> Lines);

public record OrderLineDto(
    Guid Id,
    string ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal);
