using Application.UseCases.Categories.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.GetAll
{
    public record GetAllCategoriesQuery() : IRequest<ErrorOr<IReadOnlyList<CategoryResponse>>>;
}
