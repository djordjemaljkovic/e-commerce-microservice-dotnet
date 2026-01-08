using OrderingDomain.Models;

namespace OrderingDomain.Events
{
    public record OrderCreatedEvent(Order order) : IDomainEvent;
}
