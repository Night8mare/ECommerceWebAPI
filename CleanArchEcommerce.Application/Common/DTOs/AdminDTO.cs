using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.DTOs
{
    public class AdminDTO : IMapFrom<User>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PostalCard { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
