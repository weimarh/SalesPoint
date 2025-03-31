using Application.UseCases.Waiters.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Waiters.GetAll
{
    public record GetAllWaitersQuery() : IRequest<ErrorOr<IReadOnlyList<WaiterResponse>>>;
}
