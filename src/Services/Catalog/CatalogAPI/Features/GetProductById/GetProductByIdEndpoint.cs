
using CatalogAPI.Features.CreateProduct;

namespace CatalogAPI.Features.GetProductById
{
    //no request object needed
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var res = await sender.Send(new GetProductByIdQuery(id));

                var resp = res.Adapt<GetProductByIdResponse>();

                return Results.Ok(resp);
            })
                .WithName("GetProductById")
                .Produces<CreateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id");
        }
    }
}
