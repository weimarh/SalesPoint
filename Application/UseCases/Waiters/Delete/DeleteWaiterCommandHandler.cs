using Domain.DomainErrors;
using Domain.Entities.Waiters;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.Waiters.Delete
{
    public sealed class DeleteWaiterCommandHandler : IRequestHandler<DeleteWaiterCommand, ErrorOr<Unit>>
    {
        private readonly IWaiterRepository _waiterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWaiterCommandHandler(IUnitOfWork unitOfWork, IWaiterRepository waiterRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _waiterRepository = waiterRepository ?? throw new ArgumentNullException(nameof(waiterRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteWaiterCommand command, CancellationToken cancellationToken)
        {
            if (await _waiterRepository.GetByIdAsync(new WaiterId(command.Id)) is not Waiter waiter)
                return WaiterErrors.WaiterNotFound;

            _waiterRepository.Remove(waiter);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
