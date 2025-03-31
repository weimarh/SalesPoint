using Application.UseCases.ShoppingCarts.Common;
using Domain.Entities.ShoppingCarts;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.GetAll
{
    public sealed class GetAllShoppingCartsQueryHandler : IRequestHandler<GetAllShoppingCartsQuery, ErrorOr<IReadOnlyList<ShoppingCartResponse>>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetAllShoppingCartsQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<ShoppingCartResponse>>> Handle(GetAllShoppingCartsQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<ShoppingCart> shoppingCarts = await _shoppingCartRepository.GetAllAsync();

            return shoppingCarts.Select(shoppingCart => new ShoppingCartResponse
            (
                shoppingCart.Id.Value,
                shoppingCart.Orders.Select(order => order.Product.ProductName).ToList(),
                shoppingCart.Orders.Select(order => order.Product.UnitPrice.Value).ToList(),
                shoppingCart.Orders.Select(order => order.Quantity.Value).ToList(),
                shoppingCart.Orders.Select(order => order.PartialPrice.Value).ToList(),
                shoppingCart.TotalPrice.Value,
                shoppingCart.SaleDate.ToString(),
                shoppingCart.Waiter.FullName,
                shoppingCart.User.UserName,
                shoppingCart.SaleNumber
            )).ToList();
        }
    }
}
