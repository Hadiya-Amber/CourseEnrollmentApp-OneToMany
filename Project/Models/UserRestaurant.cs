using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management.Models
{
    public class UserRestaurant
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }


    }
}
