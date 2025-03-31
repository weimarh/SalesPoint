using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Roles
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasConversion(
                roleId => roleId.Value,
                value => new RoleId(value));

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(200);
        }
    }
}
