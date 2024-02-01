using AutoMapper;
using MyFlix.Data.Dtos;
using MyFlix.Models;

namespace MyFlix.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }

    }
}
