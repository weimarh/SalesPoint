using FluentValidation;

namespace Application.UseCases.Products.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithName("El ID del producto es requerido");

            RuleFor(p => p.ProductName)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(50)
                .WithName("El nombre es requerido");

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithName("El precio es requerido");

            RuleFor(p => p.CategoryId.ToString())
                .NotEmpty()
                .WithName("La categoria es requerida");

            RuleFor(p => p.Description)
                .MaximumLength(200);
        }
    }
}
