using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Role role) =>
            await _context.Roles.AddAsync(role);

        public async Task<bool> ExistsAsync(RoleId id) =>
            await _context.Roles.AnyAsync(role => role.Id == id);

        public async Task<IReadOnlyList<Role>> GetAllAsync() =>
            await _context.Roles.ToListAsync();

        public async Task<Role?> GetByIdAsync(RoleId id) =>
            await _context.Roles.SingleOrDefaultAsync(role => role.Id == id);

        public void Remove(Role role) =>
            _context.Roles.Remove(role);

        public void Update(Role role) =>
            _context.Roles.Update(role);
    }
}
