using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<Result<List<ProductDTO>>>
    {
        public int PageNumger { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
