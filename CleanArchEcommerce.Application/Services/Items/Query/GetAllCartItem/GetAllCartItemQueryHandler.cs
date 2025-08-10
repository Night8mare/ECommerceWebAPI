using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Query.GetAllCartItem
{
    public class GetAllCartItemQueryHandler : IRequestHandler<GetAllCartItemQuery, Result<List<ItemViewDTO>>>
    {
        #region Field
        private readonly IItemRepository _itemRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetAllCartItemQueryHandler(IItemRepository itemRepository, ITokenService tokenService, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<List<ItemViewDTO>>> Handle(GetAllCartItemQuery request, CancellationToken cancellationToken)
        {
            var userEmail = _tokenService.Email;
            Log.Information($"Executing get all cart item handler by Email: {userEmail} ..");
            var item = await _itemRepository.GetAllItemIdAsync(request.cartId, request.pageNumber, request.pageSize);
            if (item == null)
            {
                Log.Error("No item available in the cart..");
                return Result<List<ItemViewDTO>>.Failure(null, "No item available in the cart",ErrorType.BadRequest);
            }
            Log.Information("Returning item list to the controller..");
            var itemList = _mapper.Map<List<ItemViewDTO>>(item);
            return Result<List<ItemViewDTO>>.Success(itemList);
        }
        #endregion
    }
}
