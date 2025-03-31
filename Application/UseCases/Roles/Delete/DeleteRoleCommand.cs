using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.Delete
{
    public record DeleteRoleCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
