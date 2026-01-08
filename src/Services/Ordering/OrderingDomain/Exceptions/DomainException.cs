namespace OrderingDomain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string msg) : base($"Domain exception : {msg} thrown from domain layer.")
        {
            
        }
    }
}
