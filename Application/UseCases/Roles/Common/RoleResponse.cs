namespace Application.UseCases.Roles.Common
{
    public record RoleResponse
    (
        Guid Id,
        string Name,
        string? Description
    );
}
