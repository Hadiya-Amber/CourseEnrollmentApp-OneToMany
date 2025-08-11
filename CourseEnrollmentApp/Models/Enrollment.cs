using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseEnrollmentApp.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        [Required]
        public string StudentName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateJoined { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        // Navigation property
        public Course Course { get; set; }
    }
}
