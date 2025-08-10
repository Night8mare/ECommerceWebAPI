using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Items.Command.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<ItemViewDTO>>
    {
        #region Field
        private readonly ICartRepository _cartRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public CreateItemCommandHandler(ICartRepository cartRepository, IItemRepository itemRepository, IProductRepository productRepository, IMapper mapper, ITokenService tokenService)
        {
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        #endregion
        #region Handler Function
        public async Task<Result<ItemViewDTO>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Starting handling item creation command..");
            var userId = _tokenService.UserId;

            Log.Information("Checking matching Cart ID with the user input");
            var check = await _cartRepository.MatchUserCartIdAsync(userId, request.CartId);
            if (!check)
            {
                Log.Error("Cart ID didn`t match the user inputting the data");
                return Result<ItemViewDTO>.Failure(null,"Unauthorized", ErrorType.Unauthorized);
            }

            Log.Information("Getting product..");
            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null || product.IsAvailable == "OutOfStock")
            {
                Log.Error("Product provided isn`t found in the database..");
                return Result<ItemViewDTO>.Failure(null ,"Product not found in the database", ErrorType.NotFound);
            }

            Log.Information("Assigning item to the DB");
            var item = new Item(
                cartId: request.CartId,
                productId: request.ProductId,
                quantity: request.Quantity,
                totalAmount: product.PurchasePrice * request.Quantity
                );

            Log.Information("Executing creating item method..");
            var itemResult = await _itemRepository.CreateItemAsync(item);
            var itemResultDto = _mapper.Map<ItemViewDTO>(itemResult);

            Log.Information("Returning item view dto to the front-end..");
            return Result<ItemViewDTO>.Success(itemResultDto);
        }
        #endregion
    }
}
