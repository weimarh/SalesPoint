using Application.UseCases.Products.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.GetAll
{
    public record GetAllProductsQuery() : IRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;
}
