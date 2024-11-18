using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TotalMarks { get; set; }
        public int TotalQuestions { get; set; }

        public bool IsDeleted { get; set; }
        [ForeignKey("Lecture")]
        public int LectureId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Lecture Lecture { get; set; }
        public Course Course { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; } = new HashSet<QuizResult>();
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}
