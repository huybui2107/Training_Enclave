using AutoMapper;
using BE.Databases.Entities;
using BE.DTOs;

namespace BE.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() {
            CreateMap<UserDto, User>();
            CreateMap<User, ResUserDto>();
        }
    }
}
