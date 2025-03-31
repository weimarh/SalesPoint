using Domain.Entities.Waiters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Waiters
{
    public class WaiterRepository : IWaiterRepository
    {
        private readonly ApplicationDbContext _context;

        public WaiterRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Waiter waiter) =>
            await _context.Waiters.AddAsync(waiter);

        public async Task<bool> ExistsAsync(WaiterId id) =>
            await _context.Waiters.AnyAsync(waiter => waiter.Id == id);

        public async Task<IReadOnlyList<Waiter>> GetAllAsync() =>
            await _context.Waiters.ToListAsync();

        public async Task<Waiter?> GetByIdAsync(WaiterId id) =>
            await _context.Waiters.SingleOrDefaultAsync(waiter => waiter.Id == id);

        public async Task<Waiter?> GetByNameAsync(string name) =>
            await _context.Waiters.SingleOrDefaultAsync(waiter => waiter.FullName == name);

        public void Remove(Waiter waiter) =>
            _context.Waiters.Remove(waiter);

        public void Update(Waiter waiter) =>
            _context.Waiters.Update(waiter);
    }
}
