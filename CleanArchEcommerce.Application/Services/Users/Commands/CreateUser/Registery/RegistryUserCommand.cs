using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.Registery
{
    public class RegistryUserCommand : IRequest<Result<UserDTO>>, IMapFrom<User>, IMapFrom<Cart>
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
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegistryUserCommand, User>();
            profile.CreateMap<User, Cart>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                .ConstructUsing(src => new Cart(
                    src.Id
                    ));
        }
    }
}
