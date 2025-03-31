using FluentValidation;

namespace Application.UseCases.Waiters.Update
{
    public class UpdateWaiterCommandValidator : AbstractValidator<UpdateWaiterCommand>
    {
        public UpdateWaiterCommandValidator()
        {
            RuleFor(w => w.Id)
                .NotEmpty();

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
