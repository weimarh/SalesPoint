using ErrorOr;
using MediatR;

namespace Application.UseCases.Products.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
