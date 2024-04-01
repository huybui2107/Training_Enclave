using System.ComponentModel.DataAnnotations;

namespace BE.DTOs
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool Gender { get; set; }
        public string Avatar { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
