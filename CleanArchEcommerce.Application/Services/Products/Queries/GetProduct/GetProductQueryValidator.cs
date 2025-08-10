using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProduct
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator() 
        {
            RuleFor(u => u.PageNumger)
                .NotEmpty().WithMessage("Page number is required.")
                .GreaterThan(0).WithMessage("Page number must be a positive integer.");
            Log.Information("Validating PageNumger field..");
        }
    }
}
