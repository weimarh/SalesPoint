using Application.UseCases.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.GetAll
{
    public record GetAllUsersQuery() : IRequest<ErrorOr<IReadOnlyList<UserResponse>>>;
}
