using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required, StringLength(20)]
        public string? Role { get; set; }
        [Required]
        public DateTime RegisteredDate { get; set; } 

        // Navigation
        public ICollection<UserRestaurant>? UserRestaurants { get; set; }
    }
}
