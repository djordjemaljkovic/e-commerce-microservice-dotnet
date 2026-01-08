
using CatalogAPI.Features.CreateProduct;

namespace CatalogAPI.Features.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest req, ISender sender) =>
            { 
                var query = req.Adapt<GetProductsQuery>();

                var result = await sender.Send(new GetProductsQuery());

                var resp = result.Adapt<GetProductsResponse>();

                return Results.Ok(resp);
            })
                .WithName("GetProducts")
                .Produces<CreateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
        }
    }
}
