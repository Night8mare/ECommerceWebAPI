using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Specifications.UserSpecifications;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using CleanArchEcommerce.Domain.RepositoryInterface.Generic;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.Registery
{
    public class RegistryUserCommandHandler : IRequestHandler<RegistryUserCommand, Result<UserDTO>>
    {
        #region Field
        private readonly IGenericRepository<User> _userRepo;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public RegistryUserCommandHandler(IGenericRepository<User> userRepo, ICartRepository cartRepository, IMapper mapper)
        {
            _userRepo = userRepo;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        #endregion
        #region Handling Function
        public async Task<Result<UserDTO>> Handle(RegistryUserCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Executing registry user");
            var user = _mapper.Map<User>(request);
            var spec = new UserGetByEmailSpecification(user.Email);
            var userExists = _userRepo.GetBySpecAsync(spec);
            if (userExists != null)
            {
                Log.Error("User email input already exists in the database");
                return Result<UserDTO>.Failure(null, "Email already Exists..", ErrorType.BadRequest);
            }

            var userResult = await _userRepo.AddAsync(user);
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
