using Application.UseCases.Categories.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<ErrorOr<CategoryResponse>>;
}
