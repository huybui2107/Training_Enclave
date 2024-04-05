using AutoMapper;
using BE.Databases.Entities;
using BE.DTOs;

namespace BE.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() {
            CreateMap<RequestUserDto, User>();
            CreateMap<User, ResUserDto>();
            CreateMap<FileDto,FileUpload>();
        }
    }
}
