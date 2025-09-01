using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Application.Common.Specifications.UserSpecifications;
using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Domain.RepositoryInterface.Generic;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserDTO>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetUserByIdQueryHandler(IUserRepository userRepository, IGenericRepository<User> userRepo, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRepo = userRepo;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<GetUserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed get user by id handler..");
            var spec = new UserGetByIdSpecification(request.UserId);
            var user = await _userRepo.GetBySpecAsync(spec);
            if (user == null)
            {
                Log.Error("User not found");
                return Result<GetUserDTO>.Failure(null, "User not found", ErrorType.NotFound);
            }
            var userMap = _mapper.Map<GetUserDTO>(user);
            return Result<GetUserDTO>.Success(userMap);
        }
        #endregion
    }
}
