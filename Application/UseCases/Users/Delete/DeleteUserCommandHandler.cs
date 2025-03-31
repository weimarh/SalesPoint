using Domain.DomainErrors;
using Domain.Entities.Users;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Delete
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(new UserId(command.Id)) is not User user)
                return UserErrors.UserNotFound;

            _userRepository.Remove(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
