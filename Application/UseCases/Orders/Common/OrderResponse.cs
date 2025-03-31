namespace Application.UseCases.Orders.Common
{
    public record OrderResponse
    (
        Guid Id,
        string ProductName,
        string Price,
        string Quantity,
        string PartialPrice
    );
}
