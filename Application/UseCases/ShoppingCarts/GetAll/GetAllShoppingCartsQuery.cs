using Application.UseCases.ShoppingCarts.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.GetAll
{
    public record GetAllShoppingCartsQuery() : IRequest<ErrorOr<IReadOnlyList<ShoppingCartResponse>>>;
}
