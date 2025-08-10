using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Orders;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Services.Orders.Queries.GetOrderHistory
{
    public class GetOrderHistoryQueryHandler : IRequestHandler<GetOrderHistoryQuery, Result<List<OrderDto>>>
    {
        #region Field
        private readonly IOrderRepository _orderRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetOrderHistoryQueryHandler(IOrderRepository orderRepository, ITokenService tokenService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<List<OrderDto>>> Handle(GetOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed get order history handler..");
            var orderHistory = await _orderRepository.GetOrderHistoryAsync(_tokenService.UserId);
            if (orderHistory == null)
            {
                Log.Error("No order history found for this user..");
                return Result<List<OrderDto>>.Failure(null, "No order history found for this user.", ErrorType.NotFound);
            }
            var orderResult = _mapper.Map<List<OrderDto>>(orderHistory);
            return Result<List<OrderDto>>.Success(orderResult);
        }
        #endregion
    }
}
