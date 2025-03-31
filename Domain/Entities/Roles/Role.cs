using Domain.Primitives;

namespace Domain.Entities.Roles
{
    public sealed class Role : AggregateRoot
    {
        private Role() { }

        public Role(RoleId id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public RoleId Id { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        public static Role UpdateRole(RoleId id, string name, string? description)
        {
            return new Role
            (
                id,
                name,
                description
            );
        }
    }
}
