using AutoMapper;
using BE.Databases;
using BE.Databases.Entities;
using BE.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BE.Service
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Createuser(RequestUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            using var hashFunc = new HMACSHA256();
            var passwordBytes = Encoding.UTF8.GetBytes(userDto.Password);
            user.PasswordHash = hashFunc.ComputeHash(passwordBytes);
            user.PasswordSalt = hashFunc.Key;
            await _context.Users.AddAsync(user);
            _context.SaveChanges();

        }

        public async Task<User?> getUserByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
        }

 
    }
}
