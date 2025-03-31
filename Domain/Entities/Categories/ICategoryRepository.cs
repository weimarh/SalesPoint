namespace Domain.Entities.Categories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(CategoryId id);
        Task<bool> ExistsAsync(CategoryId id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Remove(Category category);
    }
}
