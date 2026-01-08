
using Microsoft.EntityFrameworkCore;
using OrderingApplication.Extensions;

namespace OrderingApplication.Features.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            //Get Orders with pagination
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders.Include(x => x.OrderItems).OrderBy(x => x.OrderName.Value).Skip(pageSize * pageIndex).Take(pageSize).ToListAsync(cancellationToken);

            //Return result
            return new GetOrdersResult(new Main.Pagination.PaginationResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
        }
    }
}
