using Domain.Entities.Categories;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Products
{
    public sealed class Product : AggregateRoot
    {
        private Product() { }

        public Product(ProductId id, string name, Price price, CategoryId categoryId, Category category, string? description, string? thumbnailUrl)
        {
            Id = id;
            ProductName = name;
            UnitPrice = price;
            CategoryId = categoryId;
            Category = category;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
        }

        public ProductId Id { get; private set; } = null!;
        public string ProductName { get; private set; } = string.Empty;
        public Price UnitPrice { get; private set; } = null!;
        public CategoryId CategoryId { get; private set; } = null!;
        public Category Category { get; private set; } = null!;
        public string? Description { get; private set; }
        public string? ThumbnailUrl { get; private set; }

        public static Product UpdateProduct(ProductId id, string name, Price price, CategoryId categoryId, Category category, string? description, string? thumbnailUrl)
        {
            return new Product
            (
                id,
                name,
                price,
                categoryId,
                category,
                description,
                thumbnailUrl
            );
        }
    }
}
