using System.ComponentModel.DataAnnotations;

namespace Restuarant_Management.DTO
{
    // For User Login
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
