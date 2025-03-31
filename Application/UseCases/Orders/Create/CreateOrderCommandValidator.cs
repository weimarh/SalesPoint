using FluentValidation;

namespace Application.UseCases.Orders.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.ProductId)
                .NotEmpty()
                .WithName("El producto se requerido");

            RuleFor(o => o.Quantity)
                .NotEmpty()
                .WithName("La cantidad es requerida");
        }
    }
}
