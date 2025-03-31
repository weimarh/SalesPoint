using Application.UseCases.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.GetById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<UserResponse>>;
}
