using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
            RuleFor(u => u.CartId)
                .NotEmpty().WithMessage("Cart ID is required.")
                .GreaterThan(0).WithMessage("Cart ID must be a positive integer.");
            Log.Information("Validating Cart ID field..");
        }
    }
}
