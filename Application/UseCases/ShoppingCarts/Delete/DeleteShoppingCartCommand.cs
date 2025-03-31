using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Delete
{
    public record DeleteShoppingCartCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
