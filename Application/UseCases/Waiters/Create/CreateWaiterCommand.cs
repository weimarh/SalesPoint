using ErrorOr;
using MediatR;

namespace Application.UseCases.Waiters.Create
{
    public record CreateWaiterCommand
    (
        string FullName,
        string DNI,
        string PhoneNumber,
        DateOnly HireDate
    ) : IRequest<ErrorOr<Unit>>;
}
