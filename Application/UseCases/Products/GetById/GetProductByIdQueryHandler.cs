using Application.UseCases.Products.Common;
using Domain.DomainErrors;
using Domain.Entities.Products;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.GetById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _productRepository.GetByIdAsync(new ProductId(query.Id)) is not Product product)
            {
                return ProductErrors.ProductNotFound;
            }

            return new ProductResponse
            (
                product.Id.Value,
                product.ProductName,
                product.UnitPrice.Value,
                product.Category.CategoryName,
                product?.Description,
                product?.ThumbnailUrl
            );
        }
    }
}
