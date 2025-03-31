using Domain.Entities.Orders;
using Domain.Entities.Users;
using Domain.Entities.Waiters;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.ShoppingCarts
{
    public sealed class ShoppingCart : AggregateRoot
    {
        private ShoppingCart() { }

        public ShoppingCart(ShoppingCartId id, List<Order> orders, Price totalPrice, DateTime saleDate, WaiterId waiterId, Waiter waiter, UserId userId, User user, int saleNumber)
        {
            Id = id;
            Orders = orders;
            TotalPrice = totalPrice;
            SaleDate = saleDate;
            WaiterId = waiterId;
            Waiter = waiter;
            UserId = userId;
            User = user;
            SaleNumber = saleNumber;
        }

        public ShoppingCart(ShoppingCartId id, List<Order> orders, Price totalPrice, WaiterId waiterId, Waiter waiter)
        {
            Id = id;
            Orders = orders;
            TotalPrice = totalPrice;
            WaiterId = waiterId;
            Waiter = waiter;
        }

        public ShoppingCartId Id { get; private set; } = null!;
        public List<Order> Orders { get; private set; } = null!;
        public Price TotalPrice { get; private set; } = null!;
        public DateTime SaleDate { get; private set; }
        public WaiterId WaiterId { get; private set; } = null!;
        public Waiter Waiter { get; private set; } = null!;
        public UserId UserId { get; private set; } = null!;
        public User User { get; private set; } = null!;
        public int SaleNumber { get; private set; } = 0;

        public static ShoppingCart UpdateShoppingCart(ShoppingCartId id, List<Order> orders, Price totalPrice, WaiterId waiterId, Waiter waiter)
        {
            return new ShoppingCart
            (
                id,
                orders,
                totalPrice,
                waiterId,
                waiter
            );
        }
    }
}
