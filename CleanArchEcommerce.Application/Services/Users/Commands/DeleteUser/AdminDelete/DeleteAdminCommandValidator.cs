using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.AdminDelete
{
    public class DeleteAdminCommandValidator : AbstractValidator<DeleteAdminCommand>
    {
        public DeleteAdminCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be a positive integer.");
            Log.Information("Validating Id field..");
        }
    }
}
