using Domain.DomainErrors;
using Domain.DomainServices;
using Domain.Entities.Orders;
using Domain.Entities.ShoppingCarts;
using Domain.Entities.Users;
using Domain.Entities.Waiters;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Create
{
    public sealed class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ErrorOr<Unit>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IWaiterRepository _waiterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PriceCalculationService _priceCalculationService;

        public CreateShoppingCartCommandHandler(
            IShoppingCartRepository shoppingCartRepository,
            IOrderRepository orderRepository,
            IWaiterRepository waiterRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            PriceCalculationService priceCalculationService)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _waiterRepository = waiterRepository ?? throw new ArgumentNullException(nameof(waiterRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _priceCalculationService = priceCalculationService;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
        {
            var orders = new List<Order>();
            var prices = new List<string>();
            int saleNumber = 0;

            foreach (var id in command.OrderIds)
            {
                if (await _orderRepository.GetByIdAsync(new OrderId(id)) is not Order order)
                    return ShoppingCartErrors.OrderNotFound;

                orders.Add(order);
                prices.Add(order.PartialPrice.Value);
            }

            if (await _waiterRepository.GetByIdAsync(new WaiterId(command.WaiterId)) is not Waiter waiter)
                return ShoppingCartErrors.WaiterNotFound;

            if (await _userRepository.GetByIdAsync(new UserId(command.UserId)) is not User user)
                return ShoppingCartErrors.UserNotFound;

            var totalPriceValue = _priceCalculationService.CalculateTotalPrice(prices);
            if (totalPriceValue == null)
                return ShoppingCartErrors.InvalidTotalPrice;

            var totalPrice = Price.Create(totalPriceValue);
            if (totalPrice == null)
                return ShoppingCartErrors.InvalidTotalPrice;

            var saleDate = DateTime.UtcNow;

            var shoppingCarts = await _shoppingCartRepository.GetAllAsync();

            int numberOfShoppingcarts = shoppingCarts.Count;

            if (numberOfShoppingcarts == 0)
                saleNumber = 1;
            else
            {
                TimeSpan difference = saleDate - shoppingCarts[numberOfShoppingcarts - 1].SaleDate;

                if (difference.TotalHours > 8)
                    saleNumber = 1;
                else
                    saleNumber = shoppingCarts[numberOfShoppingcarts - 1].SaleNumber + 1;
            }

            var shoppingCart = new ShoppingCart
            (
                new ShoppingCartId(Guid.NewGuid()),
                orders,
                totalPrice,
                saleDate,
                new WaiterId(command.WaiterId),
                waiter,
                new UserId(command.UserId),
                user,
                saleNumber
            );

            await _shoppingCartRepository.AddAsync( shoppingCart );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
