using Domain.DomainErrors;
using Domain.Entities.Roles;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.Roles.Create
{
    public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ErrorOr<Unit>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                return RoleErrors.EmptyName;

            if (command.Name.Length < 4)
                return RoleErrors.NameTooShort;

            if (command.Description != null && command.Description.Length > 200)
                return RoleErrors.DescriptionTooLong;

            Role role = new Role
            (
                new RoleId(Guid.NewGuid()),
                command.Name,
                command.Description
            );

            await _roleRepository.AddAsync(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
