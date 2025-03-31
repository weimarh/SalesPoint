using Application.UseCases.Orders.Common;
using Domain.Entities.Orders;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.GetAll
{
    public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, ErrorOr<IReadOnlyList<OrderResponse>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ErrorOr<IReadOnlyList<OrderResponse>>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(order => new OrderResponse
            (
                order.Id.Value,
                order.Product.ProductName,
                order.Product.UnitPrice.Value,
                order.Quantity.Value,
                order.PartialPrice.Value
            )).ToList();
        }
    }
}
