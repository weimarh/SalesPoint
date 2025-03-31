using FluentValidation;

namespace Application.UseCases.Roles.Update
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();

            RuleFor(r => r.Name)
               .NotEmpty()
               .MinimumLength(4)
               .MaximumLength(20);

            RuleFor(r => r.Description)
                .MaximumLength(200);
        }
    }
}
