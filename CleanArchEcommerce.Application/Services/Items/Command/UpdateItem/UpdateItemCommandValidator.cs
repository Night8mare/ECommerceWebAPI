using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Command.UpdateItem
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator() 
        {
            string[] validOptions = { "Pending", "Suspended" };

            Log.Information("Executing validation for input.");
            RuleFor(u => u.CartId)
                .NotEmpty().WithMessage("Cart ID is required.")
                .GreaterThan(0).WithMessage("Cart ID must be a positive integer..");
            Log.Information("Validation in cart ID field..");

            RuleFor(u => u.ProductId)
                .NotEmpty().WithMessage("Product ID is required.")
                .GreaterThan(0).WithMessage("Product ID must be a positive integer.");
            Log.Information("Validation in product ID field..");

            RuleFor(u => u.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be a positive integer.");
            Log.Information("Validation in quantity field..");

            RuleFor(u => u.ItemStatus)
                .NotEmpty().WithMessage("Item status is required..")
                .Must(value => validOptions.Contains(value)).WithMessage($"Item status must match {validOptions[0]} or {validOptions[1]}");
            Log.Information("Validation in item status..");
        }
    }
}
