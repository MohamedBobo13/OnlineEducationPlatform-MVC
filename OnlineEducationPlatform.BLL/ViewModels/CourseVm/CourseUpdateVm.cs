using OnlineEducationPlatform.BLL.Validation.InstructorValidation;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dto.CourseDto
{
    public class CourseUpdateVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalHours { get; set; }
        [ForeignKey("Instructor")]
        [instructorIdExists]
        public string InstructorId { get; set; }
        //public Instructor Instructor { get; set; }
        //public ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
        //public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        //public ICollection<Quiz> Quizzes { get; set; } = new HashSet<Quiz>();

        //public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
    }
}
