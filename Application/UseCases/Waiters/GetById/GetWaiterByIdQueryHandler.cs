using Application.UseCases.Waiters.Common;
using Domain.DomainErrors;
using Domain.Entities.Waiters;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.UseCases.Waiters.GetById
{
    public sealed class GetWaiterByIdQueryHandler : IRequestHandler<GetWaiterByIdQuery, ErrorOr<WaiterResponse>>
    {
        private readonly IWaiterRepository _waiterRepository;

        public GetWaiterByIdQueryHandler(IWaiterRepository waiterRepository)
        {
            _waiterRepository = waiterRepository ?? throw new ArgumentNullException(nameof(waiterRepository));
        }

        public async Task<ErrorOr<WaiterResponse>> Handle(GetWaiterByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _waiterRepository.GetByIdAsync(new WaiterId(query.Id)) is not Waiter waiter)
            {
                return WaiterErrors.WaiterNotFound;
            }

            return new WaiterResponse
            (
                waiter.Id.Value,
                waiter.FullName,
                waiter.DNI,
                waiter.PhoneNumber.Value,
                waiter.HireDate.ToString()
            );
        }
    }
}
