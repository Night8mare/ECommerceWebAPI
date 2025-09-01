using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Application.Common.Specifications.UserSpecifications;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Generic;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.UpdateUser.AdminUpdate
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Result<List<string>>>
    {
        #region Field
        private readonly IGenericRepository<User> _userRepo;
        private readonly ITokenService _tokenService;
        #endregion
        #region Constructor
        public UpdateAdminCommandHandler(IGenericRepository<User> userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }
        #endregion
        #region Handler Function
        public async Task<Result<List<string>>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed update admin handler..");
            var spec = new UserGetByIdSpecification(request.Id);
            var user = await _userRepo.GetBySpecAsync(spec);
            if (user == null)
            {
                Log.Error($"User with ID: {request.Id} doesn`t exist in the database");
                return Result<List<string>>.Failure(null, "User not found", ErrorType.NotFound);
            }
            var userUpdate = user.UpdateAdminFields(request.FirstName, request.LastName, request.Email, request.PhoneNo,
                                               request.Country, request.State, request.City, request.Address, request.PostalCard, request.Password, request.Role);
            if (userUpdate.Count == 0)
            {
                Log.Information("No changes occured");
                return Result<List<string>>.Failure(null, "No changes occured", ErrorType.BadRequest);
            }

            var update = await _userRepo.UpdateAsync(user);
            if (update == 0)
            {
                Log.Error("Something went wrong while updating admin account..");
                return Result<List<string>>.Failure(null, "Something went wrong while updating admin account..", ErrorType.BadRequest);
            }
            return Result<List<string>>.Success(userUpdate);
        }
        #endregion
    }
}
