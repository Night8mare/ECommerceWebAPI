using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.AdminCreate
{
    public class AdminCreateCommand : IRequest<Result<AdminDTO>> , IMapFrom<User>, IMapFrom<Cart>
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
            profile.CreateMap<AdminCreateCommand, User>()
                .ConstructUsing(src => new User(
                    src.FirstName,
                    src.LastName,
                    src.Email,
                    src.PhoneNo,
                    src.Country,
                    src.State,
                    src.City,
                    src.Address,
                    src.PostalCard,
                    src.Password,
                    src.Role
                    ));
            profile.CreateMap<User, Cart>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ConstructUsing(src => new Cart(
                    src.Id
                    ));
        }
    }
}
