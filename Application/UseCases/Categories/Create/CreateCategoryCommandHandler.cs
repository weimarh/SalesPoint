using Domain.DomainErrors;
using Domain.Entities.Categories;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.Categories.Create
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<Unit>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.CategoryName))
                return CategoryErrors.EmptyName;

            if (command.CategoryName.Length < 4)
                return CategoryErrors.NameTooShort;

            if (command.Description != null && command.Description.Length > 200)
                return CategoryErrors.DescriptionTooLong;

            var category = new Category
            (
                new CategoryId(Guid.NewGuid()),
                command.CategoryName,
                command.Description ?? string.Empty
            );

            await _categoryRepository.AddAsync(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
