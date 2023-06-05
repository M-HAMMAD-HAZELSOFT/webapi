using AutoMapper;
using webapi.Dtos.Users;
using webapi.Models;

namespace webapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UsersDto>();
            CreateMap<UsersDto, Users>();
        }
    }
}
