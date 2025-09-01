using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Application.Common.Specifications.UserSpecifications;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Domain.RepositoryInterface.Generic;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Commands.UpdateUser.UserUpdate
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<List<string>>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public UpdateUserCommandHandler(IUserRepository userRepository, IGenericRepository<User> userRepo, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRepo = userRepo;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handling function
        public async Task<Result<List<string>>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed update user handler..");
            var spec = new UserGetByIdSpecification(_tokenService.UserId);
            var user = await _userRepo.GetBySpecAsync(spec);
            var userUpdate = user.UpdateUserFields(request.FirstName, request.LastName, request.Email, request.PhoneNo,
                                               request.Country, request.State, request.City, request.Address, request.PostalCard, request.Password);
            if (userUpdate.Count == 0)
            {
                Log.Information("No changes occuried.");
                return Result<List<string>>.Failure(null, "No changes occuried", ErrorType.BadRequest);
            }

            var update = await _userRepo.UpdateAsync(user);
            if (update == 0)
            {
                Log.Error("Something went wrong while updating user..");
                return Result<List<string>>.Failure(null, "Something went wrong while updating user..", ErrorType.BadRequest);
            }
            return Result<List<string>>.Success(userUpdate);
        }
        #endregion
    }
}
