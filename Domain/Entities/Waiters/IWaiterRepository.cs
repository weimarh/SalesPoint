using System;
using System.Collections.Generic;
namespace Domain.Entities.Waiters
{
    public interface IWaiterRepository
    {
        Task<IReadOnlyList<Waiter>> GetAllAsync();
        Task<Waiter?> GetByIdAsync(WaiterId id);
        Task<Waiter?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(WaiterId id);
        Task AddAsync(Waiter waiter);
        void Update(Waiter waiter);
        void Remove(Waiter waiter);
    }
}
