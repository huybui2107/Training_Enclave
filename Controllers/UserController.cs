using AutoMapper;
using BE.Databases;
using BE.Databases.Entities;
using BE.DTOs;
using BE.Extensions;
using BE.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace BE.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinary;

        public UserController(IUserService userService, ITokenService tokenService, IMapper mapper, ICloudinaryService cloudinary)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResUser>> SignIn (string username, string password)
        {
            if (!Extension.ValidEmail(username)) return BadRequest("The format of the email address isn't correct");


            if (!Extension.IsValidPassword(password)) return BadRequest("The format of the password isn't correct");
            var currentUser = await _userService.getUserByUserName(username);
            if (currentUser is null) return BadRequest("User is not found");
            using var hashFunc = new HMACSHA256(currentUser.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = hashFunc.ComputeHash(passwordBytes);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != currentUser.PasswordHash[i])
                    return Unauthorized("Password not match");
            }
            var res = new ResUser
            {
                Token = await _tokenService.GenerateToken(username),
            };
            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<ActionResult> SignUp(UserDto userDto)
        {

            if(!Extension.ValidEmail(userDto.Email)) return BadRequest("The format of the email address is incorrect");


            if (!Extension.IsValidPassword(userDto.Password)) return BadRequest("The format of the password isn incorrect");
            if (await _userService.getUserByUserName(userDto.Email) is not null) return BadRequest("Email is existed ...");

            
           await _userService.Createuser(userDto);
            return Ok();
        }

        [HttpPost("upload_file")]
        public async Task<ActionResult> UploadFile(IFormFile model)
        {   
            try
            {
                var urlfile = _cloudinary.uploadFile(model);
                if (urlfile == null)
                    return BadRequest("Invalid file.");

                return Ok(urlfile);

            }
            catch (Exception ex)
            {

            }


            return Ok();
        }

        [Authorize]
        [HttpGet("userinfor")]
        public async Task<ActionResult<ResUserDto>> getInfor(string username)
        {
            var currentUser = await _userService.getUserByUserName(username);
            if (currentUser == null) return BadRequest("User is not exist");
            return Ok(_mapper.Map<ResUserDto>(currentUser));
        }
    }
}
