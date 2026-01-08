using Main.Pagination;

namespace OrderingApplication.Features.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginationResult<OrderDto> Orders);
}
