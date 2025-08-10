using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<List<ProductDTO>>>
    {
        #region Field
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructure
        public GetProductQueryHandler(IProductRepository productRepository, ITokenService tokenService, IMapper mapper)
        {
            _productRepository = productRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handling Function
        public async Task<Result<List<ProductDTO>>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executing get product query handler.");
            var product = await _productRepository.GetAllProductAsync(request.PageNumger,request.PageSize);
            if (product.Count() == 0)
            {
                Log.Error("Product not found.");
                return Result<List<ProductDTO>>.Failure(null,"Product not found", ErrorType.NotFound);
            }
            var productList = _mapper.Map<List<ProductDTO>>(product);
            return Result<List<ProductDTO>>.Success(productList);
        }
        #endregion
    }
}
