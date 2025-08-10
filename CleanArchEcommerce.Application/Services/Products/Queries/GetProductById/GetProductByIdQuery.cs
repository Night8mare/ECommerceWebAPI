using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Result<ProductDTO>>
    {
        public int ProductId { get; set; }
    }
}
