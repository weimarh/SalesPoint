using Domain.DomainErrors;
using Domain.Entities.Categories;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.Update
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Unit>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (!await _productRepository.ExistsAsync(new ProductId(command.Id)))
                return ProductErrors.ProductNotFound;

            if (string.IsNullOrWhiteSpace(command.ProductName))
                return ProductErrors.EmptyName;

            if (command.ProductName.Length < 4)
                return ProductErrors.NameTooShort;

            var price = Price.Create(command.Price);

            if (price == null)
                return ProductErrors.PriceWithBadFormat;

            var category = await _categoryRepository.GetByIdAsync(new CategoryId(command.CategoryId));

            if (category == null)
                return ProductErrors.CategoryNotFound;

            if (command.Description != null && command.Description.Length > 200)
                return ProductErrors.DescriptionTooLong;

            Product product = Product.UpdateProduct
            (
                new ProductId(command.Id),
                command.ProductName,
                price,
                new CategoryId(command.CategoryId),
                category,
                command.Description ?? string.Empty,
                command.ThumbnailUrl
            );

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
