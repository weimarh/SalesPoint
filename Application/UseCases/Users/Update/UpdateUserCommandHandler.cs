using Domain.DomainErrors;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Update
{
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            if (!await _userRepository.ExistsAsync(new UserId(command.Id)))
                return UserErrors.UserNotFound;

            List<Role> roles = new List<Role>();

            if (string.IsNullOrWhiteSpace(command.UserName))
                return UserErrors.EmptyName;

            if (await _userRepository.GetByNameAsync(command.UserName) is not null)
                return UserErrors.NameAlreadyExists;

            if (command.UserName.Length < 4)
                return UserErrors.NameTooShort;

            if (string.IsNullOrWhiteSpace(command.Email))
                return UserErrors.EmptyEmail;

            if (command.RoleIds.Count == 0)
                return UserErrors.EmptyRole;

            foreach (var item in command.RoleIds)
            {
                if (await _roleRepository.GetByIdAsync(new RoleId(item)) is not Role role)
                    return UserErrors.RoleNotFound;
                roles.Add(role);
            }

            var user = new User
            (
                new UserId(Guid.NewGuid()),
                command.UserName,
                command.Email,
                roles
            );

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
