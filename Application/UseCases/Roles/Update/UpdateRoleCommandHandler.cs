using Domain.DomainErrors;
using Domain.Entities.Roles;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Roles.Update
{
    public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ErrorOr<Unit>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            if (!await _roleRepository.ExistsAsync(new RoleId(command.Id)))
                return RoleErrors.RoleNotFound;

            if (string.IsNullOrWhiteSpace(command.Name))
                return RoleErrors.EmptyName;

            if (command.Name.Length < 4)
                return RoleErrors.NameTooShort;

            if (command.Description != null && command.Description.Length > 200)
                return RoleErrors.DescriptionTooLong;

            Role role = Role.UpdateRole
            (
                new RoleId(command.Id),
                command.Name,
                command.Description
            );

            _roleRepository.Update(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
