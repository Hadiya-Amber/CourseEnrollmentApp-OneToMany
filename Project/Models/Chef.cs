using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }

        [Required, StringLength(100)]
        public string? ChefName { get; set; }

        [Range(0, 60)]
        public int ExperienceYears { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; } 

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }  

        [StringLength(100)]
        public string? Specialty { get; set; }

        // Foreign Key
        [Required]
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        // Navigation
        public ICollection<Cuisine>? Cuisines { get; set; }
    }
}
