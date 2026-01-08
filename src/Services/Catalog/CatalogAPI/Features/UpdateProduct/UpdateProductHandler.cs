using CatalogAPI.Features.CreateProduct;

namespace CatalogAPI.Features.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!").Length(2,150).WithMessage("Name must be between 2 and 150 chars");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0!");
        }
    }

    public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler called with {@Query}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if(product == null)
            {
                throw new ProductNotFound(command.Id);
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
