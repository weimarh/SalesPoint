using FluentValidation;

namespace Application.UseCases.ShoppingCarts.Create
{
    public class CreateShoppingCartCommandValidator : AbstractValidator<CreateShoppingCartCommand>
    {
        public CreateShoppingCartCommandValidator()
        {
            RuleFor(s => s.OrderIds)
                .NotEmpty();

            RuleFor(s => s.WaiterId)
                .NotEmpty();

            RuleFor(s => s.UserId)
                .NotEmpty();
        }
    }
}
