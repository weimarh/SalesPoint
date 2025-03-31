using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Delete
{
    public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
