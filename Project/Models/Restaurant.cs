using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required, StringLength(100)]
        public string? RestuarantName { get; set; }

        [Required, StringLength(200)]
        public string? Location { get; set; }

        [Required]
        public DateTime EstablishedDate { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal AverageMealPrice { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        // Navigation
        public ICollection<Chef>? Chefs { get; set; }
        public ICollection<UserRestaurant>? UserRestaurant { get; set; }
    }
}
