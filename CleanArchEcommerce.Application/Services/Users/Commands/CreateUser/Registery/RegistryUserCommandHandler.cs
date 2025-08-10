using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.Registery
{
    public class RegistryUserCommandHandler : IRequestHandler<RegistryUserCommand, Result<UserDTO>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public RegistryUserCommandHandler(IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        #endregion
        #region Handling Function
        public async Task<Result<UserDTO>> Handle(RegistryUserCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Executing registry user");
            var user = _mapper.Map<User>(request);
            var userResult = await _userRepository.RegisterUserAsync(user);
            if (userResult == null)
            {
                Log.Error("Something went wrong while registry user..");
                return Result<UserDTO>.Failure(null, "Something went wrong while registry user..", ErrorType.BadRequest);
            }
            var cart = _mapper.Map<Cart>(userResult);
            var cartResult = await _cartRepository.CreateCartAsync(cart);
            if (cartResult == null)
            {
                Log.Error("Something went wrong while cart for the user..");
                return Result<UserDTO>.Failure(null, "Something went wrong while cart for the user..", ErrorType.BadRequest);
            }
            var userMap = _mapper.Map<UserDTO>(userResult);
            return Result<UserDTO>.Success(userMap);
        }
        #endregion
    }
}
