using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                value => new OrderId(value));

            builder.Property(o => o.ProductId)
            .HasConversion(
                productId => productId.Value,
                value => new ProductId(value))
            .IsRequired();

            builder.HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.Quantity)
            .HasConversion(
                quantity => quantity.Value,
                value => Quantity.Create(value)!)
                .IsRequired();

            builder.Property(o => o.PartialPrice)
            .HasConversion(
                price => price.Value,
                value => Price.Create(value)!)
            .IsRequired();
        }
    }
}
