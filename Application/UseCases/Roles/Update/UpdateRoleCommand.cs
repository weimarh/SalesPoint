using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.Update
{
    public record UpdateRoleCommand
    (
        Guid Id,
        string Name,
        string? Description
    ) : IRequest<ErrorOr<Unit>>;
}
