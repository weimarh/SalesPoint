using ErrorOr;

namespace Domain.DomainErrors
{
    public static class WaiterErrors
    {
        public static Error EmptyName => Error.Validation(
            code: "Waiter.EmptyName",
            description: "EL nombre esta vacio"
         );

        public static Error NameAlreadyExists => Error.Validation(
            code: "Waiter.NameAlreadyExists",
            description: "El nombre ya existe"
         );

        public static Error NameTooShort => Error.Validation(
            code: "Waiter.NameTooShort",
            description: "El nombre es muy corto"
         );

        public static Error PhoneNumberWithBadFormat => Error.Validation(
            code: "Waiter.PhoneNumberWithBadFormat",
            description: "El numero de telofono no esta en el formato correcto"
         );

        public static Error EmptyDNI => Error.Validation(
            code: "Waiter.EmptyDNI",
            description: "EL Carnet de identidad esta vacio"
         );

        public static Error WaiterNotFound => Error.NotFound(
            code: "Waiter.WaiterNotFound",
            description: "No se encontró el camarero"
         );
    }
}
