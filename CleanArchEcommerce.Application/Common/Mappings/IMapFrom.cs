using AutoMapper;

namespace CleanArchEcommerce.Application.Common.Mappings
{
    public interface IMapFrom <T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
