using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.AdminDelete
{
    public class DeleteAdminCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
