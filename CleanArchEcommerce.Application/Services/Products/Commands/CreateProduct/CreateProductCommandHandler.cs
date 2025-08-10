using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Products;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler :IRequestHandler<CreateProductCommand, Result<ProductDTO>>
    {
        #region Field
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public CreateProductCommandHandler(IProductRepository productRepository, ITokenService tokenService, IMapper mapper)
        {
            _productRepository = productRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handling function
        public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} Executed create product handler");
            var productDTO = _mapper.Map<Product>(request);
            Log.Information("Executing create product function");
            var productCreated = await _productRepository.CreateProductAsync(productDTO);
            if (productCreated == null)
            {
                Log.Error("Something went wrong while creating product..");
                return Result<ProductDTO>.Failure(null, "Something went wrong while creating product..",ErrorType.BadRequest);
            }
            Log.Information("Returning product created..");
            var productResult = _mapper.Map<ProductDTO>(productCreated);
            return Result<ProductDTO>.Success(productResult);
        }
        #endregion
    }
}
