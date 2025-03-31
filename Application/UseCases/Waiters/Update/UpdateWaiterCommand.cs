using ErrorOr;
using MediatR;

namespace Application.UseCases.Waiters.Update
{
    public record UpdateWaiterCommand
    (
        Guid Id,
        string FullName,
        string DNI,
        string PhoneNumber,
        DateOnly HireDate
    ) : IRequest<ErrorOr<Unit>>;
}
