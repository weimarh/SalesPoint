using FluentValidation;

namespace Application.UseCases.ShoppingCarts.Delete
{
    public class DeleteShoppingCartCommandValidator : AbstractValidator<DeleteShoppingCartCommand>
    {
        public DeleteShoppingCartCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty();
        }
    }
}
