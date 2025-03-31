using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Order order) =>
            await _context.Orders.AddAsync(order);

        public async Task<bool> ExistsAsync(OrderId id) =>
            await _context.Orders.AnyAsync(o => o.Id == id);

        public async Task<IReadOnlyList<Order>> GetAllAsync() =>
            await _context.Orders.ToListAsync();

        public async Task<Order?> GetByIdAsync(OrderId id) =>
            await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);

        public void Remove(Order order) =>
            _context.Orders.Remove(order);

        public void Update(Order order) =>
            _context.Orders.Update(order);
    }
}
