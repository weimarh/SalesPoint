using Application.UseCases.Products.Common;
using Domain.Entities.Products;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.GetAll
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<IReadOnlyList<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Product> products = await _productRepository.GetAllAsync();

            return products.Select(product => new ProductResponse
            (
                product.Id.Value,
                product.ProductName,
                product.UnitPrice.Value,
                product.Category.CategoryName,
                product?.Description,
                product?.ThumbnailUrl
            )).ToList();
        }
    }
}
