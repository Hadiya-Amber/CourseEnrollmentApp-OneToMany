using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentApp.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        public string Description { get; set; }

        // Navigation property (One Course has many Enrollments)
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
