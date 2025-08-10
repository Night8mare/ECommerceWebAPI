using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Command.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result<int>>
    {
        #region Field
        private readonly IItemRepository _itemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public DeleteItemCommandHandler(IItemRepository itemRepository, ICartRepository cartRepository, ITokenService tokenService)
        {
            _itemRepository = itemRepository;
            _cartRepository = cartRepository;
            _tokenService = tokenService;
        }
        #endregion
        #region Handler Function
        public async Task<Result<int>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var userID = _tokenService.UserId;
            Log.Information($"Executing delete item handler for item ID: {request.ItemId} / User: {userID}..");
            var matchUser = await _cartRepository.MatchUserCartIdAsync(userID, request.CartId);
            Log.Information($"Matching user ID:{userID} with Cart ID:{request.CartId}");
            if (!matchUser)
            {
                Log.Error($"User ID :{userID} tried to delete another cart item ID {request.ItemId} with cart ID {request.CartId}..");
                return Result<int>.Failure(0,"Unauthorized Execution..", ErrorType.Unauthorized);
            }
            Log.Information($"Executing delete item function for item ID:{request.ItemId}");
            var item = await _itemRepository.DeleteItemAsync(request.ItemId);
            if (item == 0)
            {
                Log.Error($"Item with ID : {request.ItemId} NotFound..");
                return Result<int>.Failure(0,$"Item with ID : {request.ItemId} NotFound..", ErrorType.NotFound);
            }
            Log.Information($"Item with ID : {request.ItemId} deleted successfully..");
            return Result<int>.Success(1);
        }
        #endregion
    }
}
