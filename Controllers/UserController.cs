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

namespace BE.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResUser>> SignIn (string username, string password)
        {
            if (!Extension.ValidEmail(username)) return BadRequest("The format of the email address isn't correct");


            if (!Extension.IsValidPassword(password)) return BadRequest("The format of the password isn't correct");
            var currentUser = await _userService.Login(username,password);
            if (currentUser == null) return BadRequest("User is not exist");

            var res = new ResUser
            {
                Token = await _tokenService.GenerateToken(username),
            };
            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<ActionResult> SignUp(UserDto userDto)
        {

            if(!Extension.ValidEmail(userDto.Email)) return BadRequest("The format of the email address isn't correct");


            if (!Extension.IsValidPassword(userDto.Password)) return BadRequest("The format of the password isn't correct");
            if (await _userService.getUserByUserName(userDto.Email) is not null) return BadRequest("Email is existed ...");

            
           await _userService.Createuser(userDto);
            return Ok();
        }

        [HttpPost("upload")]
        public ActionResult UploadFile(string txt)
        {
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
