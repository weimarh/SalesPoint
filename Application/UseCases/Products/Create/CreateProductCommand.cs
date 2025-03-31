using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.Create
{
    public record CreateProductCommand
    (
        string ProductName,
        string Price,
        Guid CategoryId,
        string? Description,
        string? ThumbnailPath
    ) : IRequest<ErrorOr<Unit>>;
}
