using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Query.GetAllItem
{
    public class GetAllItemQueryHandler : IRequestHandler<GetAllItemQuery, Result<List<ItemViewDTO>>>
    {
        #region Field
        private readonly IItemRepository _itemRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetAllItemQueryHandler(IItemRepository itemRepository, ITokenService tokenService, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<List<ItemViewDTO>>> Handle(GetAllItemQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Executing get all item query handler..");
            var item = await _itemRepository.GetAllItemAsync(request.pageNumber, request.pageSize);
            if (item == null)
            {
                Log.Error("No item was found..");
                return Result<List<ItemViewDTO>>.Failure(null, "No Item found..", ErrorType.NotFound);
            }
            Log.Information("Returning list of items..");
            var itemList = _mapper.Map<List<ItemViewDTO>>(item);
            return Result<List<ItemViewDTO>>.Success(itemList);
        }
        #endregion
    }
}
