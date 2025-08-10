using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Items.Query.GetAllCartItem
{
    public class GetAllCartItemQuery : IRequest<Result<List<ItemViewDTO>>>
    {
        public int cartId { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; } = 10;
    }
}
