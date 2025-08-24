namespace Restuarant_Management.DTO
{
    public class CuisineDTO
    {       
        public string CuisineName { get; set; } 
        public string DishName { get; set; }    
        public decimal Price { get; set; }   
        public int ChefId { get; set; } 
        public bool IsVegetarian { get; set; }
    }

}

