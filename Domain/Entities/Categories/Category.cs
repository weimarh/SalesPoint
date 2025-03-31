using Domain.Primitives;

namespace Domain.Entities.Categories
{
    public sealed class Category : AggregateRoot
    {
        private Category() { }

        public Category(CategoryId id, string name, string? description)
        {
            Id = id;
            CategoryName = name;
            Description = description;
        }

        public CategoryId Id { get; private set; } = null!;
        public string CategoryName { get; private set; } = null!;
        public string? Description { get; private set; }

        public static Category UpdateCategory(CategoryId id, string name, string? description)
        {
            return new Category { Id = id, CategoryName = name, Description = description };
        }
    }
}
