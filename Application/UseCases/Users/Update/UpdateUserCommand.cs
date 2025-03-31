using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Update
{
    public record UpdateUserCommand
    (
        Guid Id,
        string UserName,
        string Email,
        List<Guid> RoleIds
    ) : IRequest<ErrorOr<Unit>>;
}
