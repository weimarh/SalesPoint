using Application.UseCases.Roles.Common;
using Domain.Entities.Roles;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.GetAll
{
    public sealed class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ErrorOr<IReadOnlyList<RoleResponse>>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<RoleResponse>>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<Role> roles = await _roleRepository.GetAllAsync();

            return roles.Select(role => new RoleResponse
            (
                role.Id.Value,
                role.Name,
                role.Description
            )).ToList();
        }
    }
}
