using Main.Exceptions;

namespace CatalogAPI.Exceptions
{
    public class ProductNotFound : NotFound
    {
        public ProductNotFound(Guid Id) : base("Product", Id) { }
    }
}
