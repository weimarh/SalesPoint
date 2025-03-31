using Domain.DomainErrors;
using Domain.Entities.Orders;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.Delete
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ErrorOr<Unit>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            if (await _orderRepository.GetByIdAsync(new OrderId(command.Id)) is not Order order)
                return OrderErrors.OrderNotFound;

            _orderRepository.Remove(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
