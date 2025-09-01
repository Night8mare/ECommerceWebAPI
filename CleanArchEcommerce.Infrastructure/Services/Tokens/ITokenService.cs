using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.Services.Tokens
{
    public interface ITokenService
    {
        int UserId { get; }
        string FirstName { get; }
        string Role {  get; }
        string Email { get; }
        string CreateToken(User user);
    }
}
