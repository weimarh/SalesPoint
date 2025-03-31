using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.Delete
{
    public record DeleteOrderCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
