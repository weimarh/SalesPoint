using ErrorOr;

namespace Domain.DomainErrors
{
    public static class UserErrors
    {
        public static Error EmptyName => Error.Validation(
            code: "User.EmptyName",
            description: "El nombre esta vacio"
         );

        public static Error NameAlreadyExists => Error.Validation(
            code: "User.NameAlreadyExists",
            description: "El nombre ya existe"
         );

        public static Error NameTooShort => Error.Validation(
            code: "User.NameTooShort",
            description: "El nombre es muy corto"
         );

        public static Error EmptyEmail => Error.Validation(
            code: "User.EmptyEmail",
            description: "El Email esta vacio"
         );

        public static Error EmptyRole => Error.Validation(
            code: "User.EmptyRole",
            description: "El rol esta vacio"
         );

        public static Error RoleNotFound => Error.NotFound(
            code: "User.RoleNotFound",
            description: "Rol no encontrado"
         );

        public static Error UserNotFound => Error.NotFound(
            code: "User.UserNotFound",
            description: "Usuario no encontrado"
         );
    }
}
