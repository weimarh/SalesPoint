using Application.UseCases.Waiters.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Waiters.GetById
{
    public record GetWaiterByIdQuery(Guid Id) : IRequest<ErrorOr<WaiterResponse>>;
}
