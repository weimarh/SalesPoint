using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Update
{
    public record UpdateShoppingCartCommand
    (
        Guid Id,
        List<Guid> OrderIds,
        Guid WaiterId
    ) : IRequest<ErrorOr<Unit>>;
}
