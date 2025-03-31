using Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Category category) =>
            await _context.Categories.AddAsync(category);

        public async Task<bool> ExistsAsync(CategoryId id) =>
            await _context.Categories.AnyAsync(c => c.Id == id);

        public async Task<IReadOnlyList<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(CategoryId id) =>
            await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

        public void Remove(Category category) =>
            _context.Categories.Remove(category);

        public void Update(Category category) =>
            _context.Categories.Update(category);
    }
}
