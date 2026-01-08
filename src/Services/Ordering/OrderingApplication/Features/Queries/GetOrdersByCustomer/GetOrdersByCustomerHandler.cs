
using Microsoft.EntityFrameworkCore;
using OrderingApplication.Extensions;

namespace OrderingApplication.Features.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            //Get orders by customer using dbContext
            var orders = await dbContext.Orders.Include(x => x.OrderItems).AsNoTracking().Where(x => x.CustomerId == CustomerId.Of(query.CustomerId)).OrderBy(x => x.OrderName.Value).ToListAsync(cancellationToken);

            //return result
            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
