using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProductFilter
{
    public class GetProductFilterQuery : IRequest<Result<List<ProductDTO>>>
    {
        public string? Name { get; set; }
        public decimal? PurchasePriceMin { get; set; }
        public decimal? PurchasePriceMax { get; set; }
        public string? OrderBy { get; set; }
        public int PageNumger { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
