using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Items.Command.UpdateItem
{
    public class UpdateItemCommand : IRequest<Result<int>>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ItemStatus { get; set; }
    }
}
