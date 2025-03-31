using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
