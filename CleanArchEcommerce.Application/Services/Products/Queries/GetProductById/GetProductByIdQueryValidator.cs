using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(u => u.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");
            Log.Information("Validating Product ID field..");
        }
    }
}
