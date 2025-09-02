using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAssessment.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required, StringLength(100)]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Role { get; set; }
       
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<EmpPro>? EmpPros { get; set; }

    }
}
