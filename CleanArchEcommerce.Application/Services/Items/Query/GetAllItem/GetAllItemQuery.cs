using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Items.Query.GetAllItem
{
    public class GetAllItemQuery : IRequest<Result<List<ItemViewDTO>>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; } = 10;
    }
}
