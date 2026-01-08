
namespace OrderingApplication.Features.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            //Delete ORder entity from command object
            var orderId = OrderId.Of(command.OrderId);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken : cancellationToken); //best performant method to find entry by strongly typed primary key

            if(order is null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            //Save to database
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            //Return result
            return new DeleteOrderResult(true);
        }
    }
}
