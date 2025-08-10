using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Repository.Users;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.UserDelete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public DeleteUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        #endregion
        #region Handling function
        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed delete user handler..");
            var user = await _userRepository.GetByIdAsync(_tokenService.UserId);
            if (user == null)
            {
                Log.Error("User not found..");
                return Result<bool>.Failure(false, "User not found..", ErrorType.NotFound);
            }
            var userDelete = await _userRepository.DeleteUserAsync(user.Id);
            if (userDelete == 0)
            {
                Log.Error("Something went wrong while deleting user..");
                return Result<bool>.Failure(false, "Something went wrong while deleting user..", ErrorType.BadRequest);
            }
            return Result<bool>.Success(true);
        }
        #endregion
    }
}
