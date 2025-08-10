using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Command.DeleteItem
{
    public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator() 
        {
            RuleFor(u => u.ItemId)
                .NotEmpty().WithMessage("Item ID is required.")
                .GreaterThan(0).WithMessage("Item ID must be a positive integer.");
            Log.Information("Validation in Item ID field..");

            RuleFor(u => u.CartId)
                .NotEmpty().WithMessage("Cart ID is required.")
                .GreaterThan(0).WithMessage("Cart ID must be a positive integer.");
            Log.Information("Validation in Cart ID field..");
        }
    }
}
