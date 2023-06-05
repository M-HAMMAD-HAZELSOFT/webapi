using AutoMapper;
using webapi.Dtos.Users;
using webapi.Models;

namespace webapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UsersDto>().ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<UsersDto, Users>();
        }
    }
}
