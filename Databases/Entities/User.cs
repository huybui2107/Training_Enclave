using System.ComponentModel.DataAnnotations;

namespace BE.Databases.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public string Address { get; set; } = null!;
        public bool Gender { get; set; } 

        [StringLength(500)]
        public string Avatar { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
