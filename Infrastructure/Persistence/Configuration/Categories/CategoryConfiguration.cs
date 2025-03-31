using Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasConversion(
                catId => catId.Value,
                value => new CategoryId(value));

            builder.Property(c => c.CategoryName).HasMaxLength(20).IsRequired();

            builder.Property(c => c.Description).HasMaxLength(200);
        }
    }
}
