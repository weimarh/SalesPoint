using ErrorOr;

namespace Domain.DomainErrors
{
    public static class OrderErrors
    {
        public static Error EmptyProduct => Error.Validation(
            code: "Order.EmptyProduct",
            description: "Producto vacio"
         );

        public static Error ProductNotFound => Error.NotFound(
            code: "Order.ProductNotFound",
            description: "Producto no encontrado"
         );

        public static Error EmptyQuantity => Error.Validation(
            code: "Order.EmptyQuantity",
            description: "Catidad vacia"
         );

        public static Error QuantityWithBadFormat => Error.Validation(
            code: "Order.QuantityWithBadFormat",
            description: "La cantidad no esta en el formato correcto"
         );

        public static Error OrderNotFound => Error.NotFound(
            code: "Order.OrderNotFound",
            description: "Orden no encontrado"
         );

        public static Error InvalidPriceFormat => Error.Validation(
            code: "Order.InvalidPriceFormat",
            description: "El precio no esta en el formato correcto"
         );
    }
}
