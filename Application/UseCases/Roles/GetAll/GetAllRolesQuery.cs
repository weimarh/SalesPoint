using Application.UseCases.Roles.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.GetAll
{
    public record GetAllRolesQuery() : IRequest<ErrorOr<IReadOnlyList<RoleResponse>>>;
}
