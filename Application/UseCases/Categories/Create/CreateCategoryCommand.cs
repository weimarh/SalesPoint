using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.Create
{
    public record CreateCategoryCommand
    (
        string CategoryName,
        string? Description
    ) : IRequest<ErrorOr<Unit>>;
}
