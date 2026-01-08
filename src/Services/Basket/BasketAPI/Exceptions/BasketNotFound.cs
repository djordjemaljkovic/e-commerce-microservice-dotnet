
namespace BasketAPI.Exceptions
{
    public class BasketNotFound : NotFound
    {
        public BasketNotFound(string username) : base("Basket", username)
        {
            
        }
    }
}
