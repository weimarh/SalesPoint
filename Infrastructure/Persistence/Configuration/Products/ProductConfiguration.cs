using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    productId => productId.Value,
                    value => new ProductId(value));

            builder.Property(p => p.ProductName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.UnitPrice)
                .HasConversion(
                    unitPrice => unitPrice.Value,
                    value => Price.Create(value)!)
                .IsRequired();

            builder.Property(p => p.CategoryId)
                .HasConversion(
                    categoryId => categoryId.Value,
                    value => new CategoryId(value))
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Description)
                .HasMaxLength(200);
        }
    }
}
