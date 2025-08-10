using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Services.Orders.Queries.GetOrderItemHistory
{
    public class GetOrderItemHistoryQueryHandler : IRequestHandler<GetOrderItemHistoryQuery, Result<List<ItemViewDTO>>>
    {
        #region Field
        private readonly IItemRepository _itemRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetOrderItemHistoryQueryHandler(IItemRepository itemRepository, ITokenService tokenService, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<List<ItemViewDTO>>> Handle(GetOrderItemHistoryQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed get item history handler..");
            var itemHistory = await _itemRepository.GetItemHistoryAsync(_tokenService.UserId);
            if (itemHistory == null)
            {
                Log.Error("No item found for this user..");
                return Result<List<ItemViewDTO>>.Failure(null, "No item found for this user..", ErrorType.NotFound);
            }
            var itemResult = _mapper.Map<List<ItemViewDTO>>(itemHistory);
            return Result<List<ItemViewDTO>>.Success(itemResult);
        }
        #endregion
    }
}
