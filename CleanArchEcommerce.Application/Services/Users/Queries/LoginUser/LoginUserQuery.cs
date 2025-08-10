using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<Result<UserLoginDTO>>, IMapFrom<Cart>, IMapFrom<User>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User,Cart>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
