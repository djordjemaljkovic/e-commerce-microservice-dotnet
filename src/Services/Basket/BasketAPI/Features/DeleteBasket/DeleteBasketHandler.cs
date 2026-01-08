
namespace BasketAPI.Features.DeleteBasket
{
    public record DeleteBasketCommand(string UserName)  :ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeletBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeletBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required!");
        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository repo) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
            {
                //Delete Basket from database and cache
                await repo.DeleteBasket(command.UserName, cancellationToken);

                throw new NotImplementedException();
            }
    }
}
