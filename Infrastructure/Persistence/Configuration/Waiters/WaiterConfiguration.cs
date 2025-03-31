using Domain.Entities.Waiters;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Waiters
{
    public class WaiterConfiguration : IEntityTypeConfiguration<Waiter>
    {
        public void Configure(EntityTypeBuilder<Waiter> builder)
        {
            builder.ToTable("Waiters");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .HasConversion(
                    waiterId => waiterId.Value,
                    value => new WaiterId(value))
                .IsRequired();

            builder.Property(w => w.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(w => w.DNI)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(w => w.PhoneNumber)
                .HasConversion(
                    phone => phone.Value,
                    value => PhoneNumber.Create(value)!);

            builder.Property(w => w.HireDate).IsRequired();
        }
    }
}
