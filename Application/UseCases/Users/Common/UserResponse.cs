namespace Application.UseCases.Users.Common
{
    public record UserResponse
    (
        Guid Id,
        string UserName,
        string Email,
        List<string> Roles
    );
}
