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
        public async Task<ActionResult> SignIn (string username, string password)
        {
            if (!Extension.ValidEmail(username)) return Ok(new ResError
            {
                StatusCode = "200",
                Message = "The format of the email address isn't correct",
            });


            if (!Extension.IsValidPassword(password)) return Ok(new ResError
            {
                StatusCode = "200",
                Message = "Password must have at least 8 characters and 1 uppercase letter and 1 special character.",
            });
            var currentUser = await _userService.getUserByUserName(username);
            if (currentUser is null) return  Ok(new ResError
            {
                StatusCode = "200",
                Message = "User is not found!",
            }) ;
            using var hashFunc = new HMACSHA256(currentUser.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = hashFunc.ComputeHash(passwordBytes);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != currentUser.PasswordHash[i])
                    return Ok(new ResError
                    {
                        StatusCode = "200",
                        Message = "Password is not match!",
                    });
            }
            return Ok(new ResAuthen
            {
                StatusCode = "200",
                Message = "Login is success!",
                Token = await _tokenService.GenerateToken(username),
                User = _mapper.Map<ResUserDto>(currentUser)
            }); ;
        }

        [HttpPost("register")]
        public async Task<ActionResult> SignUp(RequestUserDto userDto)
        {

            if(!Extension.ValidEmail(userDto.Email)) return Ok(new ResError
            {
                StatusCode = "200",
                Message = "The format of the email address isn't correct",
            });


            if (!Extension.IsValidPassword(userDto.Password)) return Ok(new ResError
            {
                StatusCode = "200",
                Message = "Password must have at least 8 characters and 1 uppercase letter and 1 special character.",
            });
            if (await _userService.getUserByUserName(userDto.Email) is not null) return Ok(new ResError
            {
                StatusCode = "200",
                Message = " the email address is exist",
            });


            await _userService.Createuser(userDto);
            return Ok();
        }

        [HttpPost("upload_file")]
        public async Task<ActionResult<ResFileDto>> UploadFile(IFormFile file)
        {
            try
            {
                var urlfile = await _cloudinary.uploadFile(file);
                if (urlfile == null)
                    return BadRequest("Invalid file.");

                return Ok(new ResFileDto
                {
                    StatusCode = "200",
                    Message ="Upload file successfully",
                    File = urlfile

                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet("userinfor")]
        public async Task<ActionResult<ResUser>> getInfor(string username)
        {
            var checkUsername = User.Claims.FirstOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (checkUsername != username) return Ok(new ResError
            {
                StatusCode = "200",
                Message = "The email is not matched",
            }); 
            var currentUser = await _userService.getUserByUserName(username);
            if (currentUser == null) if (checkUsername != username) return Ok(new ResError
            {
                StatusCode = "200",
                Message = "User is not exist",
            });
            return Ok(new ResUser
            {
                StatusCode = "200",
                Message = "Get user's information successfully",
                User = _mapper.Map<ResUserDto>(currentUser)
            });
        }

        [Authorize]
        [HttpGet("authenticate")]
        public async Task<ActionResult> Authen()
        {
            var userName = User.Claims.FirstOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userName is null) Ok(new ResError
            {
                StatusCode = "200",
                Message = "The email is not matched",
            });
            var currentUser = await _userService.getUserByUserName(userName);
            return Ok(new ResAuthen
            {
                StatusCode = "200",
                Message = "Authen is success!",
                Token = await _tokenService.GenerateToken(userName),
                User = _mapper.Map<ResUserDto>(currentUser)
            });
        }
    }
}
