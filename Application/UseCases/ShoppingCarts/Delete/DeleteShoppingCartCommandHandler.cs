using Domain.DomainErrors;
using Domain.Entities.ShoppingCarts;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Delete
{
    public sealed class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, ErrorOr<Unit>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteShoppingCartCommand command, CancellationToken cancellationToken)
        {
            if (await _shoppingCartRepository.GetByIdAsync(new ShoppingCartId(command.Id)) is not ShoppingCart shoppingCart)
                return ShoppingCartErrors.ShoppingCartNotFound;

            _shoppingCartRepository.Remove(shoppingCart);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
