using Domain.DomainErrors;
using Domain.Entities.Roles;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.Roles.Delete
{
    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ErrorOr<Unit>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            if (await _roleRepository.GetByIdAsync(new RoleId(command.Id)) is not Role role)
                return RoleErrors.RoleNotFound;

            _roleRepository.Remove(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
