using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Items.Command.DeleteItem
{
    public class DeleteItemCommand : IRequest<Result<int>>
    {
        public int ItemId { get; set; }
        public int CartId { get; set; }
    }
}
