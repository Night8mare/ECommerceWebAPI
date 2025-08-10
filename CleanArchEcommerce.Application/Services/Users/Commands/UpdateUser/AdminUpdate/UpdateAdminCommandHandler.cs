using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.UpdateUser.AdminUpdate
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Result<int>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public UpdateAdminCommandHandler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<int>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed update admin handler..");
            var user = _mapper.Map<User>(request);
            var userUpdate = await _userRepository.UpdateAdminAsync(user.Email, user);
            if (userUpdate == 0)
            {
                Log.Error("Something went wrong while updating admin account..");
                return Result<int>.Failure(0, "Something went wrong while updating admin account..", ErrorType.BadRequest);
            }
            return Result<int>.Success(1);
        }
        #endregion
    }
}
