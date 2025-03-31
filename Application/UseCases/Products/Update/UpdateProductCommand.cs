using Domain.Entities.Products;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.Update
{
    public record UpdateProductCommand
    (
        Guid Id,
        string ProductName,
        string Price,
        Guid CategoryId,
        string? Description,
        string? ThumbnailUrl
    ) : IRequest<ErrorOr<Unit>>;
}
