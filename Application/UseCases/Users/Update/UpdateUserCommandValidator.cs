using FluentValidation;

namespace Application.UseCases.Users.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty();

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
