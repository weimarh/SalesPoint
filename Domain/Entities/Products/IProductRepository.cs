namespace Domain.Entities.Products
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(ProductId id);
        Task<bool> ExistsAsync(ProductId id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}
