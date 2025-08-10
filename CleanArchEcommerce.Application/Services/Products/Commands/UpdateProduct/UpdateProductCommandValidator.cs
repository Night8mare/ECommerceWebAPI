using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Name is required.");
            Log.Information("Validating Name field..");

            RuleFor(u => u.Description)
                .NotEmpty().WithMessage("Description is required.");
            Log.Information("Validating Description field..");

            RuleFor(u => u.PurchasePrice)
                .NotEmpty().WithMessage("Purchase price is required.")
                .GreaterThan(0).WithMessage("Purchase price must be a positive integer.");
            Log.Information("Validating Purchase Price field..");

            RuleFor(u => u.StockQuantity)
                .NotEmpty().WithMessage("Stock quantity is required.")
                .GreaterThan(0).WithMessage("Stock quantity must be a positive integer.");
            Log.Information("Validating Stock quantity field..");

            RuleFor(u => u.IsAvailable)
                .NotEmpty().WithMessage("Is available is required.")
                .Must((product, IsAvailable) =>
                {
                    if (product.StockQuantity == 0)
                        return IsAvailable == "OutOfStock";
                    return IsAvailable == "InStock";
                }).WithMessage("Status must be 'OutOfStock' if stock is 0, otherwise 'InStock'");
            Log.Information("Validating Is available field..");
        }
    }
}
