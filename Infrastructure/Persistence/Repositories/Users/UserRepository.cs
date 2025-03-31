using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(User user) =>
            await _context.Users.AddAsync(user);

        public async Task<bool> ExistsAsync(UserId id) =>
            await _context.Users.AnyAsync(x => x.Id == id);

        public async Task<IReadOnlyList<User>> GetAllAsync() =>
            await _context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(UserId id) =>
            await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User?> GetByNameAsync(string name) =>
            await _context.Users.SingleOrDefaultAsync(x => x.UserName == name);

        public void Remove(User user) =>
            _context.Users.Remove(user);

        public void Update(User user) =>
            _context.Users.Update(user);
    }
}
