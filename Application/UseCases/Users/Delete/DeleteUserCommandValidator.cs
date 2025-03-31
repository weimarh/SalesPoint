using FluentValidation;

namespace Application.UseCases.Users.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty();
        }
    }
}
