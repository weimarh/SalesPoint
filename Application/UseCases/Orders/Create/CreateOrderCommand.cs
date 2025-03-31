using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.Create
{
    public record CreateOrderCommand
    (
        Guid ProductId,
        string Quantity
    ) : IRequest<ErrorOr<Unit>>;
}
