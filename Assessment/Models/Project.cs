using System.ComponentModel.DataAnnotations;

namespace ApiAssessment.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required, StringLength(200)]
        public string? Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

       
        public ICollection<EmpPro>? EmpPros { get; set; }
    }
}
