using AutoMapper;
using webapi.Dtos.Contact;
using webapi.Dtos.Users;
using webapi.Models;

namespace webapi
{
    /// <summary>
    /// The auto mapping profile.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UsersDto>();
            CreateMap<UsersDto, Users>();

            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
        }
    }
}
