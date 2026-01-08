

using DiscountGRPC;
using JasperFx.Events.Daemon;

namespace BasketAPI.Features.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart is not null!");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required!");
        }
    }

    public class StoreBasketCommandHandler(IBasketRepository repo, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await ApplyDiscount(command.Cart, cancellationToken);

            //Repository Pattern
            await repo.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(command.Cart.UserName);
        }

        private async Task ApplyDiscount(ShoppingCart Cart, CancellationToken cancellationToken)
        {
            //Communicate with DiscountGRPC to calculate the discount price
            foreach (var item in Cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
