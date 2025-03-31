using ErrorOr;

namespace Domain.DomainErrors
{
    public static class CategoryErrors
    {
        public static Error EmptyName => Error.Validation(
            code: "Category.EmptyName",
            description: "El nombre esta vacio"
         );

        public static Error NameTooShort => Error.Validation(
            code: "Category.NameTooShort",
            description: "El nombre es muy corto"
         );

        public static Error DescriptionTooLong => Error.Validation(
            code: "Category.DescriptionTooLong",
            description: "La descripcion es muy larga"
         );

        public static Error CategoryNotFound => Error.NotFound(
            code: "Category.ProductNotFound",
            description: "Categoria no encontrado"
         );
    }
}
