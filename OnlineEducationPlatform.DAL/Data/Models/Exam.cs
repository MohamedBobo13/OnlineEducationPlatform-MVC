using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public int DurationMinutes { get; set; }
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
        public int TotalQuestions { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();
    }
}
