using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Create
{
    public record CreateShoppingCartCommand
    (
        List<Guid> OrderIds,
        Guid WaiterId,
        Guid UserId
    ) : IRequest<ErrorOr<Unit>>;
}
