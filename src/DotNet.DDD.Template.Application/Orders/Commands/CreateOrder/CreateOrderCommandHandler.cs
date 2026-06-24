using DotNet.DDD.Template.Domain.Aggregates.Orders;
using DotNet.DDD.Template.Domain.Common;
using DotNet.DDD.Template.Infrastructure.Persistence;
using DotNet.DDD.Template.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DotNet.DDD.Template.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly OrderRepository _orders;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(
        OrderRepository orders,
        IUnitOfWork unitOfWork,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _orders = orders;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.CustomerId);

        await _orders.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Order {OrderId} created for customer {CustomerId}",
            order.Id, request.CustomerId);

        return order.Id;
    }
}
