using Application.UseCases.Orders.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.GetAll
{
    public record GetAllOrdersQuery() : IRequest<ErrorOr<IReadOnlyList<OrderResponse>>>;
}
