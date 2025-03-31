using Domain.Entities.ShoppingCarts;
using Domain.Entities.Users;
using Domain.Entities.Waiters;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.ShoppingCarts
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("ShoppingCarts");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    cartId => cartId.Value,
                    value => new ShoppingCartId(value))
                .IsRequired();

            builder.HasMany(s => s.Orders)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.TotalPrice)
            .HasConversion(
                price => price.Value,
                value => Price.Create(value)!)
            .IsRequired();

            builder.Property(s => s.SaleDate)
            .IsRequired();

            builder.Property(s => s.WaiterId)
            .HasConversion(
                waiterId => waiterId.Value,
                value => new WaiterId(value))
            .IsRequired();

            builder.HasOne(s => s.Waiter)
            .WithMany() 
            .HasForeignKey(s => s.WaiterId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.UserId)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value));

            builder.HasOne(s => s.User)
                .WithMany() 
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.SaleNumber)
                .IsRequired();
        }
    }
}
