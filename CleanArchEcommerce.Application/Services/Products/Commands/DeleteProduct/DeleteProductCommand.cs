using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
}
