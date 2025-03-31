using Application.UseCases.Products.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ErrorOr<ProductResponse>>;
}
