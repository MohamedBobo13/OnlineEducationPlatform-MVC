using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = new bool();
        public DateTime? EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}

public enum EnrollmentStatus
{

    Enrolled,
    Inprogress,
    Completed,
    removed
}
