using MediatR;

namespace DotNet.DDD.Template.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(string CustomerId) : IRequest<Guid>;
