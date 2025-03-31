using ErrorOr;

namespace Domain.DomainErrors
{
    public static class RoleErrors
    {
        public static Error EmptyName => Error.Validation(
            code: "Role.EmptyName",
            description: "El nombre esta vacio"
         );

        public static Error NameTooShort => Error.Validation(
            code: "Role.NameTooShort",
            description: "El nombre es muy corto"
         );

        public static Error DescriptionTooLong => Error.Validation(
            code: "Role.DescriptionTooLong",
            description: "La descripcion es muy larga"
         );

        public static Error RoleNotFound => Error.NotFound(
            code: "Role.RoleNotFound",
            description: "Rol no encontrado"
         );
    }
}
