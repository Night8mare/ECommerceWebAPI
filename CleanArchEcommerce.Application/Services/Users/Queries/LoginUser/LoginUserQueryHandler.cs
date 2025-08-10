using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<UserLoginDTO>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public LoginUserQueryHandler(IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        #endregion
        #region Handling Function
        public async Task<Result<UserLoginDTO>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Executing login user handler..");
            var login = await _userRepository.GetUserByEmailAsync(request.Email);
            
            if (login == null)
            {
                Log.Error("Couldn`t find user");
                return Result<UserLoginDTO>.Failure(null, "Couldn`t find user.", ErrorType.BadRequest);
            }

            var verifyPassword = login.VerifyPassword(request.Password);
            if (!verifyPassword)
            {
                Log.Error("Password didn`t match");
                return Result<UserLoginDTO>.Failure(null, "Invalid Email or password", ErrorType.BadRequest);
            }

            var Token = _tokenService.CreateToken(login);
            login.SetToken(Token);
            var updateResult = await _userRepository.UpdatingTokenAsync(login, Token);
            if (updateResult == 0)
            {
                Log.Error("Something went wrong while updating token");
                return Result<UserLoginDTO>.Failure(null, "Something went wrong while updating token", ErrorType.BadRequest);
            }
            var cart = await _cartRepository.GetCartByIdAsync(login.Id);
            if (cart == null)
            {
                Log.Information("If not already created/ creating user cart");
                var cartMA = _mapper.Map<Cart>(login);
                var cartResult = await _cartRepository.CreateCartAsync(cartMA);
            }
            var loginMap = _mapper.Map<UserLoginDTO>(login);
            return Result<UserLoginDTO>.Success(loginMap);
        }
        #endregion
    }
}
