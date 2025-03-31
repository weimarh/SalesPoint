using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.Update
{
    public record UpdateOrderCommand
    (
        Guid OrderId,
        Guid ProductId,
        string Quantity
    ) : IRequest<ErrorOr<Unit>>;
}
