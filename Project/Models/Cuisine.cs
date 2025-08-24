using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management.Models
{
    public class Cuisine
    {
        [Key]
        public int CuisineId { get; set; }

        [Required, StringLength(100)]
        public string? CuisineName { get; set; }

        [Required, StringLength(100)]
        public string? DishName { get; set; }  

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  

        public bool IsVegetarian { get; set; }

        // Foreign Key
        [Required]
        public int ChefId { get; set; }
        public Chef? Chef { get; set; }
    }
}
