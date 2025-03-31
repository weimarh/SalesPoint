using Application.UseCases.Categories.Common;
using Domain.Entities.Categories;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.GetAll
{
    public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ErrorOr<IReadOnlyList<CategoryResponse>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<CategoryResponse>>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<Category> categories = await _categoryRepository.GetAllAsync();

            return categories.Select(category => new CategoryResponse
            (
                category.Id.Value,
                category.CategoryName,
                category.Description
            )).ToList();
        }
    }
}
