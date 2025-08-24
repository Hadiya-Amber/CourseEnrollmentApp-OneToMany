using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management.DTO
{
    public class ChefDTO
    {
        public string? ChefName { get; set; }

        public int ExperienceYears { get; set; }

        public DateTime JoinedDate { get; set; }    

        public decimal Salary { get; set; }   

        public string? Specialty { get; set; }

        public int RestaurantId { get; set; }
    }
}
