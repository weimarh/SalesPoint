using Domain.DomainErrors;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.Products.Create
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Unit>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.ProductName))
                return ProductErrors.EmptyName;

            if (command.ProductName.Length < 4)
                return ProductErrors.NameTooShort;

            if (string.IsNullOrWhiteSpace(command.Price))
                return ProductErrors.EmptyPrice;

            if (string.IsNullOrWhiteSpace(command.CategoryId.ToString()))
                return ProductErrors.EmptyCategory;

            var category = await _categoryRepository.GetByIdAsync(new CategoryId(command.CategoryId));
            if (category == null)
                return ProductErrors.CategoryNotFound;

            if (Price.Create(command.Price) is not Price price)
                return ProductErrors.PriceWithBadFormat;

            if (command.Description != null && command.Description.Length > 200)
                return ProductErrors.DescriptionTooLong;

            var product = new Product
            (
                new ProductId(Guid.NewGuid()),
                command.ProductName,
                price,
                new CategoryId(command.CategoryId),
                category,
                command.Description ?? string.Empty,
                command.ThumbnailPath
            );

            await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
