using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Create
{
    public record CreateUserCommand
    (
        string UserName,
        string Email,
        List<Guid> RoleIds
    ) : IRequest<ErrorOr<Unit>>;
}
