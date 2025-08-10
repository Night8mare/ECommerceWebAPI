using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.UpdateUser.UserUpdate
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public UpdateUserCommandHandler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handling function
        public async Task<Result<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed update user handler..");
            var user = _mapper.Map<User>(request);
            var userUpdate = await _userRepository.UpdateUserAsync(user.Email, user);
            if (userUpdate == 0)
            {
                Log.Error("Something went wrong while updating user..");
                return Result<int>.Failure(0, "Something went wrong while updating user..", ErrorType.BadRequest);
            }
            return Result<int>.Success(0);
        }
        #endregion
    }
}
