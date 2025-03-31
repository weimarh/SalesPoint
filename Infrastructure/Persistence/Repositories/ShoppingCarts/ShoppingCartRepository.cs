using Domain.Entities.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.ShoppingCarts
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(ShoppingCart cart) =>
            await _context.ShoppingCarts.AddAsync(cart);

        public async Task<bool> ExistsAsync(ShoppingCartId id) =>
            await _context.ShoppingCarts.AnyAsync(cart => cart.Id == id);

        public async Task<IReadOnlyList<ShoppingCart>> GetAllAsync() =>
            await _context.ShoppingCarts.ToListAsync();

        public async Task<ShoppingCart?> GetByIdAsync(ShoppingCartId id) =>
            await _context.ShoppingCarts.SingleOrDefaultAsync(cart => cart.Id == id);

        public void Remove(ShoppingCart cart) =>
            _context.ShoppingCarts.Remove(cart);

        public void Update(ShoppingCart cart) =>
            _context.ShoppingCarts.Update(cart);
    }
}
