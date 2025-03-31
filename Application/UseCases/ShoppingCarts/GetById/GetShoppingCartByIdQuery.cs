using Application.UseCases.ShoppingCarts.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.GetById
{
    public record GetShoppingCartByIdQuery(Guid Id) : IRequest<ErrorOr<ShoppingCartResponse>>;
}
