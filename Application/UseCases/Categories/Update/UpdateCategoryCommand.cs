using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.Update
{
    public record UpdateCategoryCommand
    (
        Guid Id,
        string CategoryName,
        string? Description
    ) : IRequest<ErrorOr<Unit>>;
}
