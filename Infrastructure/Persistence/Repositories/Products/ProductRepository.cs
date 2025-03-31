using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Product product) =>
            await _context.Products.AddAsync(product);

        public async Task<bool> ExistsAsync(ProductId id) =>
            await _context.Products.AnyAsync(p => p.Id == id);

        public async Task<IReadOnlyList<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(ProductId id) =>
            await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        public void Remove(Product product) => 
            _context.Products.Remove(product);

        public void Update(Product product) =>
            _context.Products.Update(product);
    }
}
