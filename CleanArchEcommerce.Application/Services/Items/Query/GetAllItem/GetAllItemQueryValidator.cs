using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Query.GetAllItem
{
    public class GetAllItemQueryValidator : AbstractValidator<GetAllItemQuery>
    {
        public GetAllItemQueryValidator() 
        {
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
