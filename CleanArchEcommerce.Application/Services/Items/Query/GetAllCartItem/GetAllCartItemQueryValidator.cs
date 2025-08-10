using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Query.GetAllCartItem
{
    public class GetAllCartItemQueryValidator : AbstractValidator<GetAllCartItemQuery>
    {
        public GetAllCartItemQueryValidator() 
        {
            RuleFor(u => u.cartId)
                .NotEmpty().WithMessage("Cart ID is required.")
                .GreaterThan(0).WithMessage("Cart ID must be a positive integer.");
            Log.Information("Validating Cart Id field..");

            RuleFor(u => u.pageNumber)
                .NotEmpty().WithMessage("Page number is required.")
                .GreaterThan(0).WithMessage("Page number must be a positive integer.");
            Log.Information("Validating page number field..");

            RuleFor(u => u.pageSize)
                .GreaterThan(0).WithMessage("Page size must be a positive integer.");
            Log.Information("Validating page size field..");
        }
    }
}
