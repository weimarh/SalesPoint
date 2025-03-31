using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<User?> GetByIdAsync(UserId id);
        Task<User?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(UserId id);
        Task AddAsync(User user);
        void Update(User user);
        void Remove(User user);
    }
}
