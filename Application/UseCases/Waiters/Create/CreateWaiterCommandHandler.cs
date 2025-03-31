using Domain.DomainErrors;
using Domain.Entities.Waiters;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Waiters.Create
{
    public sealed class CreateWaiterCommandHandler : IRequestHandler<CreateWaiterCommand, ErrorOr<Unit>>
    {
        private readonly IWaiterRepository _waiterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWaiterCommandHandler(IUnitOfWork unitOfWork, IWaiterRepository waiterRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _waiterRepository = waiterRepository ?? throw new ArgumentNullException(nameof(waiterRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateWaiterCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.FullName))
                return WaiterErrors.EmptyName;

            if (command.FullName.Length < 4)
                return WaiterErrors.NameTooShort;

            if (await _waiterRepository.GetByNameAsync(command.FullName) is not Waiter)
                return WaiterErrors.NameAlreadyExists;

            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                return WaiterErrors.PhoneNumberWithBadFormat;

            if (string.IsNullOrWhiteSpace(command.DNI))
                return WaiterErrors.EmptyDNI;

            Waiter waiter = new Waiter
            (
                new WaiterId(Guid.NewGuid()),
                command.FullName,
                command.DNI,
                phoneNumber,
                command.HireDate
            );

            await _waiterRepository.AddAsync(waiter);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
