using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAssessment.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required,StringLength(100)]
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }

        [ForeignKey("Employee")]
        public int? ManagerId { get; set; }
       
        public ICollection<Employee>? Employees { get; set; }


    }
}
