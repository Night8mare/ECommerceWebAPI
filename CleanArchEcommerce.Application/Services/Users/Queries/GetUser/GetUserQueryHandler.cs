using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Domain.Repository.Users;
using MediatR;
using Serilog;

namespace CleanArchEcommerce.Application.Services.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<List<GetUserDTO>>>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public GetUserQueryHandler (IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion
        #region Handling function
        public async Task<Result<List<GetUserDTO>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Executing Get user query handler.");
            var user = await _userRepository.GetAllUserAsync(request.pageNumber,request.pageSize);
            if (user.Count() == 0)
            {
                Result<List<GetUserDTO>>.Failure(null, "Users not found..", ErrorType.NotFound);
            }
            var userList = _mapper.Map<List<GetUserDTO>>(user);
            return Result<List<GetUserDTO>>.Success(userList);
        }
        #endregion
    }
}
