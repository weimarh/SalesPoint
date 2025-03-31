using Domain.DomainErrors;
using Domain.Entities.Waiters;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.Waiters.Update
{
    public sealed class UpdateWaiterCommandHandler : IRequestHandler<UpdateWaiterCommand, ErrorOr<Unit>>
    {
        private readonly IWaiterRepository _waiterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWaiterCommandHandler(IUnitOfWork unitOfWork, IWaiterRepository waiterRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _waiterRepository = waiterRepository ?? throw new ArgumentNullException(nameof(waiterRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateWaiterCommand command, CancellationToken cancellationToken)
        {
            if (!await _waiterRepository.ExistsAsync(new WaiterId(command.Id)))
                return WaiterErrors.WaiterNotFound;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber);

            if (phoneNumber is not PhoneNumber)
                return WaiterErrors.PhoneNumberWithBadFormat;

            Waiter waiter = Waiter.UpdateWaiter
            (
                new WaiterId(command.Id),
                command.FullName,
                command.DNI,
                phoneNumber,
                command.HireDate
            );

            _waiterRepository.Update(waiter);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
