using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.DTOs
{
    public class GetUserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNo { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string PostalCard { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Token { get; private set; }
    }
}
