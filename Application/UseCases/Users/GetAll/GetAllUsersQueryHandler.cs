using Application.UseCases.Users.Common;
using Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.GetAll
{
    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<IReadOnlyList<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<UserResponse>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            List<UserResponse> usersResponse = new List<UserResponse>();

            foreach (var user in users)
            {
                List<string> roles = [];

                foreach (var item in user.Roles)
                {
                    roles.Add(item.ToString() ?? string.Empty);
                }

                usersResponse.Add(
                    new UserResponse
                    (
                        user.Id.Value,
                        user.UserName,
                        user.Email,
                        roles
                    ));
            }

            return usersResponse;
        }
    }
}
