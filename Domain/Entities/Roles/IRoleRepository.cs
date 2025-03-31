namespace Domain.Entities.Roles
{
    public interface IRoleRepository
    {
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(RoleId id);
        Task<bool> ExistsAsync(RoleId id);
        Task AddAsync(Role role);
        void Update(Role role);
        void Remove(Role role);
    }
}
