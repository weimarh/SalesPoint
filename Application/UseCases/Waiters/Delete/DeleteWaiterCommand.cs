using ErrorOr;
using MediatR;

namespace Application.UseCases.Waiters.Delete
{
    public record DeleteWaiterCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
