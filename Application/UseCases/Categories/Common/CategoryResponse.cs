namespace Application.UseCases.Categories.Common
{
    public record CategoryResponse
    (
        Guid Id,
        string CategoryName,
        string? Description
    );
}
