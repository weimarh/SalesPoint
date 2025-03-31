namespace Domain.Entities.Orders
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(OrderId id);
        Task<bool> ExistsAsync(OrderId id);
        Task AddAsync(Order order);
        void Update(Order order);
        void Remove(Order order);
    }
}
