using AutoMapper;
using BE.Databases;
using BE.Databases.Entities;
using BE.DTOs;
using Microsoft.EntityFrameworkCore;

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
        public async Task Createuser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
           await _context.Users.AddAsync(user);
            _context.SaveChanges();

        }

        public async Task<User?> getUserByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
        }

        public async Task<User?> Login(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == username && u.Password == password);
        }
    }
}
