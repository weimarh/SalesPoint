using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.Create
{
    public record CreateRoleCommand
    (
        string Name,
        string? Description
    ) : IRequest<ErrorOr<Unit>>;
}
