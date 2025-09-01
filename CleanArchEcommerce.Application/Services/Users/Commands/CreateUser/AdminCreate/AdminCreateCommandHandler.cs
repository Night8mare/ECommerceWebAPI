using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Application.Common.Specifications.UserSpecifications;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using CleanArchEcommerce.Domain.RepositoryInterface.Generic;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.AdminCreate
{
    public class AdminCreateCommandHandler : IRequestHandler<AdminCreateCommand, Result<AdminDTO>>
    {
        #region Field
        private readonly IGenericRepository<User> _userRepo;
        private readonly ITokenService _tokenService;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public AdminCreateCommandHandler(
            IGenericRepository<User> userRepo, 
            ITokenService tokenService, 
            IMapper mapper, 
            ICartRepository cartRepository)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }
        #endregion
        #region Handling function
        public async Task<Result<AdminDTO>> Handle(AdminCreateCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed admin create handler");            
            var Admin = _mapper.Map<User>(request);
            var spec = new UserGetByEmailSpecification(Admin.Email);
            var adminExist = await _userRepo.GetBySpecAsync(spec);
            if (adminExist != null)
            {
                Log.Error("Admin email input already exists in the database..");
                return Result<AdminDTO>.Failure(null, "This email already Exists..", ErrorType.BadRequest);
            }

            var adminResult = await _userRepo.AddAsync(Admin);
            if (adminResult == null)
            {
                Log.Error("Admin couldn`t be created.");
                return Result<AdminDTO>.Failure(null, "Admin couldn`t be created", ErrorType.BadRequest);
            }

            var cart = _mapper.Map<Cart>(adminResult);
            var CartResult = await _cartRepository.CreateCartAsync(cart);
            if (CartResult == null)
            {
                Log.Error("Cart couldn`t be created.");
                return Result<AdminDTO>.Failure(null, "Cart couldn`t be created.", ErrorType.BadRequest);
            }

            var adminMap = _mapper.Map<AdminDTO>(adminResult);
            return Result<AdminDTO>.Success(adminMap);
        }
        #endregion
    }
}
