using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using CleanArchEcommerce.Domain.RepositoryInterface.Orders;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
    {
        #region Field
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository,
            ITokenService tokenService, IMapper mapper, IItemRepository itemRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _itemRepository = itemRepository;
            _cartRepository = cartRepository;
        }
        #endregion
        #region Handler Function
        public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userID = _tokenService.UserId;
            Log.Information($"Executing create order handler by user:{_tokenService.Email}");
            var userMatch = await _cartRepository.MatchUserCartIdAsync(userID, request.CartId);
            if (!userMatch)
            {
                Log.Error($"User: {_tokenService.Email} tried to create an order for cart ID:{request.CartId}");
                return Result<OrderDto>.Failure(null, "Unauthorized access", ErrorType.Unauthorized);
            }
            Log.Information($"Getting all items in cart ID: {request.CartId}");
            var cartItemList = await _itemRepository.GetAllCartItemAsync(request.CartId);
            if (cartItemList.Count() == 0)
            {
                Log.Error($"No Items in Cart ID:{request.CartId}.");
                return Result<OrderDto>.Failure(null, "No Items in Cart", ErrorType.BadRequest);
            }
            Log.Information("Executing checking item availability");
            var ProductExist = await _itemRepository.CheckItemAvailabilityAsync(request.CartId);
            if (ProductExist)
            {
                Log.Error("Product found Out of stock.");
                return Result<OrderDto>.Failure(null, "Product found Out of stock", ErrorType.BadRequest);
            }
            Log.Information("Executing checking item quantity");
            var ProductQuantityAvail = await _itemRepository.CheckItemQuantityAsync(request.CartId);
            if (ProductQuantityAvail)
            {
                Log.Error("Product quantity isn`t available.");
                return Result<OrderDto>.Failure(null, "Product quantity isn`t available.", ErrorType.BadRequest);
            }
            decimal totalPrice = await _itemRepository.GetTotalPriceAsync(request.CartId);
            var order = new Order(
                cartId: request.CartId,
                totalAmount: totalPrice
                );
            Log.Information("Executing creating order function");
            var orderCreate = await _orderRepository.CreateOrderAsync(order);
            if (orderCreate == null)
            {
                Log.Error("Something went wrong when creating the order.");
                return Result<OrderDto>.Failure(null, "Something went wrong when creating the order.", ErrorType.BadRequest);
            }

            foreach (var item in cartItemList)
            {
                Log.Information($"Item List:{item.Product.Name}");
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product.StockQuantity - item.Quantity == 0)
                {
                    product.UpdateProductAvaible(item.Quantity,"OutOfStock");
                    Log.Information($"Item List after update:{item.Product.IsAvailable}");
                    await _productRepository.UpdateProductAsync(product.Id, product);
                }
                else
                {
                    product.UpdateProductQuantity(item.Quantity);
                    Log.Information($"Item List after update:{item.Product.IsAvailable}");
                    await _productRepository.UpdateProductAsync(product.Id, product);
                }

                item.UpdateItemOrder(orderCreate.Id);
                Log.Information($"Updateing item in Cart ID: {request.CartId} with order ID: {orderCreate.Id}");
                await _itemRepository.UpdateItemOrderAsync(item.CartId, item);
            }

            var orderResult = _mapper.Map<OrderDto>(orderCreate);
            Log.Information($"Returning order result ID:{orderCreate.Id}");
            return Result<OrderDto>.Success(orderResult);
        }
        #endregion
    }
}
