using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProductFilter
{
    public class GetProductFilterQueryValidator : AbstractValidator<GetProductFilterQuery>
    {
        public GetProductFilterQueryValidator()
        {
            RuleFor(u => u.PurchasePriceMin)
                .GreaterThan(0).WithMessage("Purchase price min must be greater than 0.");
            Log.Information("Validating Purchase price min field..");

            RuleFor(u => u.PurchasePriceMax)
                .GreaterThan(0).WithMessage("Purchase price max must be greater than 0.")
                .GreaterThan(u => u.PurchasePriceMin).WithMessage("Purchase price max must be greater than purchase price min.");
            Log.Information("Validating Purchase price max field..");

            RuleFor(u => u.PageNumger)
                .NotEmpty().WithMessage("Page number is required.")
                .GreaterThan(0).WithMessage("Page number must be a positive integer.");
            Log.Information("Validating Page number field..");
        }
    }
}
