using Domain.Entities.Roles;
using Domain.Primitives;

namespace Domain.Entities.Users
{
    public sealed class User : AggregateRoot
    {
        private User() { }

        public User(UserId id, string userName, string email, List<Role> roles)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Roles = roles;
        }

        public UserId Id { get; private set; } = null!;
        public string UserName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public List<Role> Roles { get; private set; } = null!;

        public static User UpdateUser(UserId id, string userName, string email, List<Role> roles)
        {
            return new User
            (
                id,
                userName,
                email,
                roles
            );
        }
    }
}
