using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.UserDelete
{
    public class DeleteUserCommand : IRequest<Result<bool>>
    {
    }
}
