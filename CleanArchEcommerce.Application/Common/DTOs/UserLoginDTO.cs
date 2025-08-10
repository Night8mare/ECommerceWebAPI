using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.DTOs
{
    public class UserLoginDTO : IMapFrom<User>
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
