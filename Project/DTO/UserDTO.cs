using System.ComponentModel.DataAnnotations;

namespace Restuarant_Management.DTO
{
    public class UserDTO
    {
        public string Username { get; set; } 
        public string? Password { get; set; }    
        public string? Role { get; set; }     
        public DateTime RegisteredDate { get; set; } 
    }

}
