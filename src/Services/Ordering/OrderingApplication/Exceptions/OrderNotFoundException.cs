
using Main.Exceptions;

namespace OrderingApplication.Exceptions
{
    public class OrderNotFoundException : NotFound
    {
        public OrderNotFoundException(Guid id) : base("Order", id)
        {
        }
    }
}
