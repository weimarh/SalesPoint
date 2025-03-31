using ErrorOr;

namespace Domain.DomainErrors
{
    public static class ProductErrors
    {
        public static Error EmptyName => Error.Validation(
            code: "Product.EmptyName",
            description: "El nombre esta vacio"
         );

        public static Error NameTooShort => Error.Validation(
            code: "Product.NameTooShort",
            description: "El nombre es muy corto"
         );

        public static Error EmptyPrice => Error.Validation(
            code: "Product.EmptyPrice",
            description: "El precio esta vacio"
         );

        public static Error PriceWithBadFormat => Error.Validation(
            code: "Product.PriceWithBadFormat",
            description: "TEl precio no esta en el formato correcto"
         );

        public static Error EmptyCategory => Error.Validation(
            code: "Product.EmptyCategory",
            description: "La categoria esta vacia"
         );

        public static Error CategoryNotFound => Error.NotFound(
            code: "Product.CategoryNotFound",
            description: "Categoria no encontrada"
         );

        public static Error DescriptionTooLong => Error.Validation(
            code: "Product.DescriptionTooLong",
            description: "La descripcion es muy larga"
         );

        public static Error ProductNotFound => Error.NotFound(
            code: "Product.ProductNotFound",
            description: "Producto no encontrado"
         );
    }
}
