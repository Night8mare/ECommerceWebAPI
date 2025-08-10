using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<GetUserDTO>>
    {
        public int UserId { get; set; }
    }
}
