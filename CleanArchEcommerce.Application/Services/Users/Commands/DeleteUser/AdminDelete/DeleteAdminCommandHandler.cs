using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Application.Common.Specifications.UserSpecifications;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Domain.RepositoryInterface.Generic;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.AdminDelete
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Result<bool>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _userRepo;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public DeleteAdminCommandHandler(
            IUserRepository userRepository,
            IGenericRepository<User> userRepo,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _userRepo = userRepo;
            _tokenService = tokenService;
        }
        #endregion
        #region Handling function
        public async Task<Result<bool>> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed delete admin handler..");
            var spec = new UserGetByIdSpecification(request.Id);
            var Admin = await _userRepo.GetBySpecAsync(spec);
            if (Admin == null)
            {
                Log.Error("Couldn`t find that user..");
                return Result<bool>.Failure(false, "Couldn`t find that user..", ErrorType.NotFound);
            }
            var deleteUser = await _userRepo.DeleteWithSpecAsync(spec);
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
