
using Microsoft.EntityFrameworkCore;
using OrderingApplication.Extensions;

namespace OrderingApplication.Features.Queries.GetOrderByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            //Get Orders by name using dbContext
            var orders = await dbContext.Orders.AsNoTracking().Where(x => x.OrderName.Value.Contains(query.Name)).OrderBy(x => x.OrderName.Value).ToListAsync(cancellationToken);

            //return result
            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
