using AutoMapper;
using webapi.Dtos.Users;
using webapi.Models;

namespace webapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, GetUserDto>();
            CreateMap<AddUserDto, Users>();
        }
    }
}
