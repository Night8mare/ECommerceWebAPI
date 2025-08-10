using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Command.UpdateItem
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result<int>>
    {
        #region Field
        private readonly ICartRepository _cartRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService; 
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public UpdateItemCommandHandler(ICartRepository cartRepository,IItemRepository itemRepository, IProductRepository productRepository, ITokenService tokenService, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
            _productRepository = productRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<int>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Executing updating item handler..");
            var userId = _tokenService.UserId;
            Log.Information($"Executing matching function for user ID:{userId}/ cart ID: {request.CartId}");
            var userMatch = await _cartRepository.MatchUserCartIdAsync(userId, request.CartId);
            if (!userMatch)
            {
                Log.Error($"User ID: {userId} tried deleteing another cart ID: {request.CartId}");
                return Result<int>.Failure(0, "Unauthorized access", ErrorType.Unauthorized);
            }
            Log.Information($"Getting the product ID:{request.ProductId}");
            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null)
            {
                Log.Error($"Product ID:{request.ProductId} wasn`t found");
                return Result<int>.Failure(0, "Product Not Found", ErrorType.NotFound);
            }
            Log.Information($"Checking if item exist in cart ID: {request.CartId}");
            var itemExist = await _itemRepository.ItemExistAsync(request.CartId, request.ProductId);
            if (!itemExist)
            {
                Log.Error($"Product ID:{request.ProductId} wasn`t found in the user cart ID: {request.CartId}..");
                return Result<int>.Failure(0, "Product Not Found", ErrorType.NotFound);
            }
            var item = new Item(
                cartId: request.CartId,
                productId: request.ProductId,
                quantity: request.Quantity,
                totalAmount: product.PurchasePrice * request.Quantity,
                itemStatus: request.ItemStatus
                );
            Log.Information("Executing updating item function..");
            var itemUpdate = await _itemRepository.UpdateItemAsync(request.CartId, item);
            if (itemUpdate == 0)
            {
                Log.Error("Something went wrong while updating item..");
                return Result<int>.Failure(0, "Item couldn`t be updated..", ErrorType.BadRequest);
            }
            Log.Information("Item updated successfully..");
            return Result<int>.Success(1);
        }
        #endregion
    }
}
