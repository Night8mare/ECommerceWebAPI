using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator() 
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be a positive integer.");
            Log.Information("Validating User ID field.");
        }
    }
}
