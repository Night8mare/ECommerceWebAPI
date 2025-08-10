using FluentValidation;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be a positive integer.");
            Log.Information("Validating Id field..");
        }
    }
}
