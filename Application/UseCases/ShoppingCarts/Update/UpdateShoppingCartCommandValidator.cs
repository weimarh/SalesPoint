using FluentValidation;

namespace Application.UseCases.ShoppingCarts.Update
{
    public class UpdateShoppingCartCommandValidator : AbstractValidator<UpdateShoppingCartCommand>
    {
        public UpdateShoppingCartCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty();

            RuleFor(s => s.OrderIds)
                .NotEmpty();

            RuleFor(s => s.WaiterId)
                .NotEmpty();
        }
    }
}
