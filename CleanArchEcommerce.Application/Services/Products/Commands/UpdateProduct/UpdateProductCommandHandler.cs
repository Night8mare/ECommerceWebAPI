using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<int>>
    {
        #region Field
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public UpdateProductCommandHandler(IProductRepository productRepository, ITokenService tokenService, IMapper mapper)
        {
            _productRepository = productRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handling function
        public async Task<Result<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executing update product handler..");
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            if (product == null)
            {
                Log.Error($"Product ID: {request.Id} wasn`t found..");
                return Result<int>.Failure(0, "Product not found in the database.", ErrorType.NotFound);
            }
            var productResult = _mapper.Map<Product>(request);
            var updateProduct = await _productRepository.UpdateProductAsync(request.Id, productResult);
            if (updateProduct == 0)
            {
                Log.Error($"Product ID: {request.Id} something went wrong while updating.");
                return Result<int>.Failure(0, "Something went wrong while updating.", ErrorType.BadRequest);
            }
            return Result<int>.Success(updateProduct);
        }
        #endregion
    }
}
