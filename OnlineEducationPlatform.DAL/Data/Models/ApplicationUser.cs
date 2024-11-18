using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public TypeUser UserType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class Student : ApplicationUser
    {
        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public ICollection<AnswerResult> AnswerResults { get; set; } = new HashSet<AnswerResult>();


        public ICollection<QuizResult> quizResults { get; set; } = new HashSet<QuizResult>();
        public ICollection<ExamResult> examResults { get; set; } = new HashSet<ExamResult>();




    }
    public class Instructor : ApplicationUser
    {


        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();


    }
    public class Admin : ApplicationUser { }
}

public enum TypeUser
{
    Admin = 1,
    Instructor = 2,
    Student = 3
}

