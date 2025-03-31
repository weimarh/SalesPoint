using Domain.DomainErrors;
using Domain.Entities.Categories;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.Update
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ErrorOr<Unit>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (!await _categoryRepository.ExistsAsync(new CategoryId(command.Id)))
                return CategoryErrors.CategoryNotFound;

            if (string.IsNullOrWhiteSpace(command.CategoryName))
                return CategoryErrors.EmptyName;

            if (command.CategoryName.Length < 4)
                return CategoryErrors.NameTooShort;

            if (command.Description != null && command.Description.Length > 200)
                return CategoryErrors.DescriptionTooLong;

            Category category = Category.UpdateCategory
            (
                new CategoryId(command.Id),
                command.CategoryName,
                command.Description ?? string.Empty
            );

            _categoryRepository.Update(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
