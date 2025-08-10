using CleanArchEcommerce.Domain.Repository.Users;
using FluentValidation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Services.Users.Queries.LoginUser
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator(IUserRepository _user)
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.");
            Log.Information("Validating Email field.");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.");
            Log.Information("Validating Password field.");
        }
    }
}
