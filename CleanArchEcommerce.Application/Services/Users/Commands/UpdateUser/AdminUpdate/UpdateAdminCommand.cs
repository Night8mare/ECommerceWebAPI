using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Commands.UpdateUser.AdminUpdate
{
    public class UpdateAdminCommand : IRequest<Result<int>>, IMapFrom<User>
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
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAdminCommand, User>();
        }
    }
}
