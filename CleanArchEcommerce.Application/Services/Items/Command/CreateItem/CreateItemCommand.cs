using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Items.Command.CreateItem
{
    public class CreateItemCommand : IRequest<Result<ItemViewDTO>>, IMapFrom<Item>
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
