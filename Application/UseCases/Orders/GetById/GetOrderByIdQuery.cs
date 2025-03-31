using Application.UseCases.Orders.Common;
using Domain.Entities.Orders;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.GetById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<ErrorOr<OrderResponse>>;
}
