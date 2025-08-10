using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProductFilter
{
    public class GetProductFilterQueryHandler : IRequestHandler<GetProductFilterQuery, Result<List<ProductDTO>>>
    {
        #region Field
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetProductFilterQueryHandler(IProductRepository productRepository, ITokenService tokenService, IMapper mapper)
        {
            _productRepository = productRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handling function
        public async Task<Result<List<ProductDTO>>> Handle(GetProductFilterQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed get product filter handler.");
            var product = await _productRepository.GetAllProductFilterAsync(request.Name, request.PurchasePriceMin, request.PurchasePriceMax, request.OrderBy, request.PageNumger, request.PageSize);
            if (product == null)
            {
                Log.Error("No product found");
                return Result<List<ProductDTO>>.Failure(null, "No Product found", ErrorType.NotFound);
            }
            var productList = _mapper.Map<List<ProductDTO>>(product);
            return Result<List<ProductDTO>>.Success(productList);
        }
        #endregion
    }
}
