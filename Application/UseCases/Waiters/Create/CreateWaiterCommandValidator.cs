using FluentValidation;

namespace Application.UseCases.Waiters.Create
{
    public class CreateWaiterCommandValidator : AbstractValidator<CreateWaiterCommand>
    {
        public CreateWaiterCommandValidator()
        {
            RuleFor(w => w.FullName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(w => w.DNI)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(10);

            RuleFor(w => w.PhoneNumber)
                .NotEmpty();
        }
    }
}
