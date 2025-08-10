using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");
            Log.Information("Validating Name field..");

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Description is required.");
            Log.Information("Validating Description field..");

            RuleFor(v => v.PurchasePrice)
                .NotEmpty().WithMessage("Purchase price is required.")
                .NotEqual(0).WithMessage("Stock quantity must be => 1.");
            Log.Information("Validating Purchase Price field..");

            RuleFor(v => v.StockQuantity)
                .NotEmpty().WithMessage("Stock quantity is required.")
                .GreaterThan(0).WithMessage("Stock quantity must be => 1.");
            Log.Information("Validating Stock Quantity field..");

            RuleFor(v => v.IsAvailable)
                .NotEmpty().WithMessage("Is available is required.")
                .Equal("InStock").WithMessage("Is available must be set to InStock.");
            Log.Information("Validating Is Available field..");
        }
    }
}
