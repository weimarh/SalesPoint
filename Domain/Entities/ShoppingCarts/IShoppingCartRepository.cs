namespace Domain.Entities.ShoppingCarts
{
    public interface IShoppingCartRepository
    {
        Task<IReadOnlyList<ShoppingCart>> GetAllAsync();
        Task<ShoppingCart?> GetByIdAsync(ShoppingCartId id);
        Task<bool> ExistsAsync(ShoppingCartId id);
        Task AddAsync(ShoppingCart cart);
        void Update(ShoppingCart cart);
        void Remove(ShoppingCart cart);
    }
}
