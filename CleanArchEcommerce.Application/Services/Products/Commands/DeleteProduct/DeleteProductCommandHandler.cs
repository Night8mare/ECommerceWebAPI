using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
    {
        #region Field
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public DeleteProductCommandHandler(IProductRepository productRepository, ITokenService tokenService)
        {
            _productRepository = productRepository;
            _tokenService = tokenService;
        }
        #endregion
        #region Handling function
        public async Task<Result<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed delete product handler..");
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            if (product == null)
            {
                Log.Error($"Product ID: {request.Id} wasn`t found..");
                return Result<int>.Failure(0,"Product not found..", ErrorType.NotFound);
            }
            var productResult = await _productRepository.DeleteProductAsync(request.Id);
            if (productResult == 0)
            {
                return Result<int>.Failure(0, "Something went wrong while deleting product", ErrorType.BadRequest);
            }
            Log.Information($"Product ID: {request.Id} deleted successfully..");
            return Result<int>.Success(productResult);
        }
        #endregion
    }
}
