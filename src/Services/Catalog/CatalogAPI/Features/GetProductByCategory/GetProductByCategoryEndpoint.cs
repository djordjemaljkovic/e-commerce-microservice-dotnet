
using CatalogAPI.Features.CreateProduct;

namespace CatalogAPI.Features.GetProductByCategory
{
    //no request object needed
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async(string category, ISender sender) =>
            {
                var res = await sender.Send(new GetProductByCategoryQuery(category));

                var resp = res.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(resp);
            })
                .WithName("GetProductByCategory")
                .Produces<CreateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Category")
                .WithDescription("Get Product By Category");
        }
    }
}
