using Application.UseCases.Users.Common;
using Domain.DomainErrors;
using Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.GetById
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(new UserId(query.Id)) is not User user)
                return UserErrors.UserNotFound;

            List<string> roles = [];

            foreach (var item in user.Roles)
            {
                roles.Add(item.ToString() ?? string.Empty);
            }

            return new UserResponse
            (
                user.Id.Value,
                user.UserName,
                user.Email,
                roles
            );
        }
    }
}
