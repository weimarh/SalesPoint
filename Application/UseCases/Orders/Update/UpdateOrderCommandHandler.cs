using Domain.DomainErrors;
using Domain.DomainServices;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Orders.Update
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ErrorOr<Unit>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PriceCalculationService _priceCalculationService;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IOrderRepository orderRepository, PriceCalculationService priceCalculationService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _priceCalculationService = priceCalculationService;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            if (!await _orderRepository.ExistsAsync(new OrderId(command.OrderId)))
                return OrderErrors.OrderNotFound;

            if (string.IsNullOrWhiteSpace(command.ProductId.ToString()))
                return OrderErrors.EmptyProduct;

            if (await _productRepository.GetByIdAsync(new ProductId(command.ProductId)) is not Product product)
                return OrderErrors.ProductNotFound;

            if (string.IsNullOrWhiteSpace(command.Quantity))
                return OrderErrors.EmptyQuantity;

            if (Quantity.Create(command.Quantity) is not Quantity quantity)
                return OrderErrors.QuantityWithBadFormat;

            var partialPrice = _priceCalculationService.CalculatePartialPrice(product.UnitPrice, quantity);

            if (partialPrice is null)
                return OrderErrors.InvalidPriceFormat;

            if (partialPrice is null)
                return OrderErrors.InvalidPriceFormat;

            var order = Order.UpdateOrder
            (
                new OrderId(command.OrderId),
                product,
                quantity,
                partialPrice
            );

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
