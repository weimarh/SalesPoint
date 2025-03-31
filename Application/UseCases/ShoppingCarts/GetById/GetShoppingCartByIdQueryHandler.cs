using Application.UseCases.ShoppingCarts.Common;
using Domain.DomainErrors;
using Domain.Entities.ShoppingCarts;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.GetById
{
    public sealed class GetShoppingCartByIdQueryHandler : IRequestHandler<GetShoppingCartByIdQuery, ErrorOr<ShoppingCartResponse>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetShoppingCartByIdQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        public async Task<ErrorOr<ShoppingCartResponse>> Handle(GetShoppingCartByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _shoppingCartRepository.GetByIdAsync(new ShoppingCartId(query.Id)) is not ShoppingCart shoppingCart)
                return ShoppingCartErrors.ShoppingCartNotFound;

            return new ShoppingCartResponse
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
            );
        }
    }
}
