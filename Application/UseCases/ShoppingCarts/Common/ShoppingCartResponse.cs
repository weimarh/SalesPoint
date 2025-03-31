namespace Application.UseCases.ShoppingCarts.Common
{
    public record ShoppingCartResponse
    (
        Guid Id,
        List<string> ProductNames,
        List<string> UnitPrices,
        List<string> Quantities,
        List<string> PartialPrices,
        string TotalPrice,
        string SalesDate,
        string WaiterName,
        string UserName,
        int SaleNumber
    );
}
