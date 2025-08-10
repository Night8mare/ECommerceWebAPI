using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDTO>>
    {
        #region Field
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructure
        public GetProductByIdQueryHandler(IProductRepository productRepository, ITokenService tokenService, IMapper mapper)
        {
            _productRepository = productRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handling function
        public async Task<Result<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed get product by ID handler..");

            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null)
            {
                Log.Error("No product found");
                return Result<ProductDTO>.Failure(null, "No product found", ErrorType.NotFound);
            }
            var productResult = _mapper.Map<ProductDTO>(product);
            return Result<ProductDTO>.Success(productResult);
        }
        #endregion
    }
}
