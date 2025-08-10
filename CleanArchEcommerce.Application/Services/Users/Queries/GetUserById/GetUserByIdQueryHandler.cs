using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using CleanArchEcommerce.Domain.Repository.Users;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserDTO>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetUserByIdQueryHandler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion
        #region Handler Function
        public async Task<Result<GetUserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"User: {_tokenService.Email} executed get user by id handler..");
            var user = await _userRepository.GetByIdAsync(request.UserId);
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
