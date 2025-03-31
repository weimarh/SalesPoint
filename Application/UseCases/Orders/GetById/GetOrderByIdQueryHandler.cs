using Application.UseCases.Orders.Common;
using Domain.DomainErrors;
using Domain.Entities.Orders;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.GetById
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ErrorOr<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<ErrorOr<OrderResponse>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _orderRepository.GetByIdAsync(new OrderId(query.Id)) is not Order order)
                return OrderErrors.OrderNotFound;

            return new OrderResponse
            (
                order.Id.Value,
                order.Product.ProductName,
                order.Product.UnitPrice.Value,
                order.Quantity.Value,
                order.PartialPrice.Value
            );
        }
    }
}
