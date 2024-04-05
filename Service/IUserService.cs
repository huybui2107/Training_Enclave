using BE.Databases.Entities;
using BE.DTOs;

namespace BE.Service
{
    public interface IUserService
    {
        Task Createuser(RequestUserDto userDto);
        Task<User?> getUserByUserName(string username);

    }
}
