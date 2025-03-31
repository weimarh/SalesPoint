namespace Application.UseCases.Waiters.Common
{
    public record WaiterResponse
    (
        Guid Id,
        string FullName,
        string DNI,
        string PhoneNumber,
        string HireDate
    );
}
