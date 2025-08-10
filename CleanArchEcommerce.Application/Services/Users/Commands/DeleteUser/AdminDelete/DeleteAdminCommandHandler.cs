using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Repository.Users;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.AdminDelete
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Result<bool>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public DeleteAdminCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        #endregion
        #region Handling function
        public async Task<Result<bool>> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed delete admin handler..");
            var Admin = await _userRepository.GetByIdAsync(request.Id);
            if (Admin == null)
            {
                Log.Error("Couldn`t find that user..");
                return Result<bool>.Failure(false, "Couldn`t find that user..", ErrorType.NotFound);
            }
            var deleteUser = await _userRepository.DeleteUserAsync(request.Id);
            if (deleteUser == 0)
            {
                Log.Error("Something went wrong while deleting user..");
                return Result<bool>.Failure(false, "Something went wrong while deleting user..", ErrorType.BadRequest);
            }
            return Result<bool>.Success(true);
        }
        #endregion
    }
}
