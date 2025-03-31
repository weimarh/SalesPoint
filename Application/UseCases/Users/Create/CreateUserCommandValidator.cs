using FluentValidation;

namespace Application.UseCases.Users.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.RoleIds)
                .NotEmpty();
        }
    }
}
