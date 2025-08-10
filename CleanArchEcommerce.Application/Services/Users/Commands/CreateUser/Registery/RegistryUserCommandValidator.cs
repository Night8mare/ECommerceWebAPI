using CleanArchEcommerce.Domain.Repository.Users;
using FluentValidation;
using Serilog;
using System.Text.RegularExpressions;

namespace CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.Registery
{
    public class RegistryUserCommandValidator : AbstractValidator<RegistryUserCommand>
    {
        public RegistryUserCommandValidator(IUserRepository _user)
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required");
            Log.Information("Validating First name field..");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required");
            Log.Information("Validating Last name field..");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Must be an Email address only")
                .MustAsync(async (email, cancellation) => !await _user.EmailExistAsync(email)).WithMessage("Email already exists!");
            Log.Information("Validating Email field..");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Address is required");
            Log.Information("Validating Address field..");

            RuleFor(u => u.City)
                .NotEmpty().WithMessage("City is required");
            Log.Information("Validating City field..");

            RuleFor(u => u.Country)
                .NotEmpty().WithMessage("Country is required");
            Log.Information("Validating Country field..");

            RuleFor(u => u.PhoneNo)
                .NotEmpty().WithMessage("PhoneNo is required")
                .Must(value => value != null && value.StartsWith("+")).WithMessage("International code is required");
            Log.Information("Validating PhoneNo field..");

            RuleFor(u => u.PostalCard)
                .NotEmpty().WithMessage("Postal card number is required");
            Log.Information("Validating Postal card field..");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                .Must(value => value != null && value.Count() >= 8).WithMessage("Password should be more than 8 characters")
                .Must(value => value != null && Regex.IsMatch(value, "[A-Z]")).WithMessage("Password should contains at least 1 capital letter")
                .Must(value => value != null && Regex.IsMatch(value, "[1-9]")).WithMessage("Password should contains at least 1 number.");
            Log.Information("Validating Password field..");
        }
    }
}
