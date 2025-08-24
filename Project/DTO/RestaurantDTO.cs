using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management.DTO
{
    public class RestaurantDTO
    {
        public string? RestuarantName { get; set; }

        public string? Location { get; set; }

        public DateTime EstablishedDate { get; set; }

        public decimal AverageMealPrice { get; set; }  
        public int Rating { get; set; }
    }
}
        