using FluentValidation;

namespace Application.UseCases.Roles.Delete
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithName("El ID es requerido");
        }
    }
}
