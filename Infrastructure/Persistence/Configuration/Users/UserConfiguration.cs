using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasConversion(
                userId => userId.Value,
                value => new UserId(value)
            ).IsRequired();

            builder.Property(u => u.UserName).IsRequired();

            builder.Property(u => u.Email).IsRequired();

            builder.HasMany(u => u.Roles)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
