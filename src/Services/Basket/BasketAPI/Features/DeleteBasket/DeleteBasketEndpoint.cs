
using BasketAPI.Features.StoreBasket;

namespace BasketAPI.Features.DeleteBasket
{
    //no request needed
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{username}", async(string username, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(username));

                var resp = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(resp);
            })
                .WithName("DeleteBasket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket");
        }
    }
}
