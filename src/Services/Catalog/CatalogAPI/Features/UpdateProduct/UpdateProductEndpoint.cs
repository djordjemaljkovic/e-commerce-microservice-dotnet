
using CatalogAPI.Features.CreateProduct;

namespace CatalogAPI.Features.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async(UpdateProductRequest req, ISender sender) =>
            {
                var command = req.Adapt<UpdateProductCommand>();

                var res = await sender.Send(command);

                var resp = res.Adapt<UpdateProductResponse>();

                return Results.Ok(resp);
            })
                .WithName("UpdateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Product")
                .WithDescription("Update Product");
        }
    }
}
