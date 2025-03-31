using Application.UseCases.Roles.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.GetById
{
    public record GetRoleByIdQuery(Guid Id) : IRequest<ErrorOr<RoleResponse>>;
}
