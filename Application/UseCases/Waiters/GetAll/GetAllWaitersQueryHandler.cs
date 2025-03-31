using Application.UseCases.Waiters.Common;
using Domain.Entities.Waiters;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Waiters.GetAll
{
    public sealed class GetAllWaitersQueryHandler : IRequestHandler<GetAllWaitersQuery, ErrorOr<IReadOnlyList<WaiterResponse>>>
    {
        private readonly IWaiterRepository _waiterRepository;

        public GetAllWaitersQueryHandler(IWaiterRepository waiterRepository)
        {
            _waiterRepository = waiterRepository ?? throw new ArgumentNullException(nameof(waiterRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<WaiterResponse>>> Handle(GetAllWaitersQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<Waiter> waiters = await _waiterRepository.GetAllAsync();

            return waiters.Select(waiter => new WaiterResponse
            (
                waiter.Id.Value,
                waiter.FullName,
                waiter.DNI,
                waiter.PhoneNumber.Value,
                waiter.HireDate.ToString()
            )).ToList();
        }
    }
}
