

namespace BasketAPI.Features.GetBasket
{
    //no request object
    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", async(string username, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(username));

                var resp =  result.Adapt<GetBasketResponse>();

                return Results.Ok(resp);
            })
                .WithName("GetBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Basket")
                .WithDescription("Get Basket");
        }
    }
}
