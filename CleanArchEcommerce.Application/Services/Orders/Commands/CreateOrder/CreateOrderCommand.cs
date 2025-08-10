using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<OrderDto>>
    {
        public int CartId { get; set; }
    }
}
