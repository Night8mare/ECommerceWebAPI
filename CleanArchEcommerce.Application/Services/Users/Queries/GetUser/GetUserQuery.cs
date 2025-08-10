using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<Result<List<GetUserDTO>>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; } = 10;
    }
}
