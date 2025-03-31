using Domain.DomainErrors;
using Domain.Entities.Categories;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Categories.Delete
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ErrorOr<Unit>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.GetByIdAsync(new CategoryId(command.Id)) is not Category category) 
                return CategoryErrors.CategoryNotFound;

            _categoryRepository.Remove(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
