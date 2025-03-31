namespace Application.UseCases.Products.Common
{
    public record ProductResponse
    (
        Guid Id,
        string ProductName,
        string Price,
        string Category,
        string? Description,
        string? ThumbnailPath
    );
}
