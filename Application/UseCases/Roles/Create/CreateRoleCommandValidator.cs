using FluentValidation;

namespace Application.UseCases.Roles.Create
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(20);

            RuleFor(r => r.Description)
                .MaximumLength(200);
        }
    }
}
