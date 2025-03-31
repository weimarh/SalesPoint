using Application.UseCases.Categories.Common;
using Domain.DomainErrors;
using Domain.Entities.Categories;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.GetById
{
    public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ErrorOr<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ErrorOr<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.GetByIdAsync(new CategoryId(query.Id)) is not Category category)
                return CategoryErrors.CategoryNotFound;

            return new CategoryResponse
            (
                category.Id.Value,
                category.CategoryName,
                category.Description
            );
        }
    }
}
