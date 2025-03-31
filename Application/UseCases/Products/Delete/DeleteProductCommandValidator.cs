using FluentValidation;

namespace Application.UseCases.Products.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithName("Se requiere el ID del producto");
        }
    }
}
