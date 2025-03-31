using Application.UseCases.Roles.Common;
using Domain.DomainErrors;
using Domain.Entities.Roles;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.GetById
{
    public sealed class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, ErrorOr<RoleResponse>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<ErrorOr<RoleResponse>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _roleRepository.GetByIdAsync(new RoleId(query.Id)) is not Role role)
                return RoleErrors.RoleNotFound;

            return new RoleResponse
            (
                role.Id.Value,
                role.Name,
                role.Description
            );
        }
    }
}
