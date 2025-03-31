using ErrorOr;

namespace Domain.DomainErrors
{
    public static class ShoppingCartErrors
    {
        public static Error EmptyUser => Error.Validation(
            code: "ShooppingCart.EmptyUser",
            description: "Campo usuario vacio"
         );

        public static Error EmptyWaiter => Error.Validation(
            code: "ShooppingCart.EmptyWaiter",
            description: "Se necesita ingresar un camarero"
         );

        public static Error InvalidTotalPrice => Error.Validation(
            code: "ShooppingCart.InvalidTotalPrice",
            description: "Precio no esta en el formato correcto"
         );

        public static Error ShoppingCartNotFound => Error.NotFound(
            code: "ShooppingCart.ShoppingCartNotFound",
            description: "Venta no encontrada"
         );

        public static Error OrderNotFound => Error.NotFound(
            code: "ShooppingCart.OrderNotFound",
            description: "Orden no encontrado"
         );

        public static Error WaiterNotFound => Error.NotFound(
            code: "ShooppingCart.WaiterNotFound",
            description: "Camarero no encontrado"
         );

        public static Error UserNotFound => Error.NotFound(
            code: "ShooppingCart.UserNotFound",
            description: "Usuario no encontrado"
         );
    }
}
