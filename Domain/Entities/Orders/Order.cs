using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Orders
{
    public sealed class Order : AggregateRoot
    {
        private Order() { }

        public Order(OrderId id, Product product, Quantity quantity, Price partialPrice)
        {
            Id = id;
            ProductId = product.Id;
            Product = product;
            Quantity = quantity;
            PartialPrice = partialPrice;
        }

        public OrderId Id { get; private set; } = null!;
        public ProductId ProductId { get; private set; } = null!;
        public Product Product { get; private set; } = null!;
        public Quantity Quantity { get; private set; } = null!;
        public Price PartialPrice { get; private set; } = null!;

        public static Order UpdateOrder(OrderId id, Product product, Quantity quantity, Price partialPrice)
        {
            return new Order
            (
                id,
                product,
                quantity,
                partialPrice
            );
        }
    }
}
