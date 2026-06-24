using DotNet.DDD.Template.Domain.Aggregates.Orders;

namespace DotNet.DDD.Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Concrete, query-specific repository for the Order aggregate.
/// Lives in Infrastructure — no domain interface to implement.
/// Add query methods here as the domain grows; never expose IQueryable.
/// </summary>
public class OrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) => _context = context;

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Orders
            .Include(o => o.Lines)
            .FirstOrDefaultAsync(o => o.Id == id, ct);

    public async Task<List<Order>> GetAllAsync(CancellationToken ct = default)
        => await _context.Orders
            .Include(o => o.Lines)
            .ToListAsync(ct);

    public async Task<List<Order>> GetByCustomerAsync(string customerId, CancellationToken ct = default)
        => await _context.Orders
            .Include(o => o.Lines)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(ct);

    public async Task<List<Order>> GetByStatusAsync(OrderStatus status, CancellationToken ct = default)
        => await _context.Orders
            .Include(o => o.Lines)
            .Where(o => o.Status == status)
            .ToListAsync(ct);

    public async Task AddAsync(Order order, CancellationToken ct = default)
        => await _context.Orders.AddAsync(order, ct);

    public void Update(Order order)
        => _context.Orders.Update(order);

    public void Remove(Order order)
        => _context.Orders.Remove(order);
}
