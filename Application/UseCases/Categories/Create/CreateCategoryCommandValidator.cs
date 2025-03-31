using FluentValidation;

namespace Application.UseCases.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20)
                .WithName("La categoria es requerida");

            RuleFor(c => c.Description)
                .MaximumLength (200)
                .WithName ("La descripcion no puede tener mas de 200 caracteres");
        }
    }
}
