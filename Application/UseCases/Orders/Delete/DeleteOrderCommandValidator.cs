using FluentValidation;

namespace Application.UseCases.Orders.Delete
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(o => o.Id)
                .NotEmpty()
                .WithName("El ID es requerido");
        }
    }
}
