using FluentValidation;

namespace Application.UseCases.Waiters.Delete
{
    public class DeleteWaiterCommandValidator : AbstractValidator<DeleteWaiterCommand>
    {
        public DeleteWaiterCommandValidator()
        {
            RuleFor(w => w.Id)
                .NotEmpty();
        }
    }
}
