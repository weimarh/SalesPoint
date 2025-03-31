using FluentValidation;

namespace Application.UseCases.Orders.Update
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty()
                .WithName("El ID de la orden es requerido");

            RuleFor(o => o.ProductId)
                .NotEmpty()
                .WithName("El ID del producto es requerido");

            RuleFor(o => o.Quantity)
                .NotEmpty()
                .WithName("La cantidad es requerida");
        }
    }
}
