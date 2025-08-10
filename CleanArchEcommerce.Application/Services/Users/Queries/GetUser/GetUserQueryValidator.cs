using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator() 
        {
            RuleFor(u => u.pageNumber)
                .NotEmpty().WithMessage("Page number is required.")
                .GreaterThan(0).WithMessage("Page number must be a positive integer.");
            Log.Information("Validating page number field.");
        }
    }
}
