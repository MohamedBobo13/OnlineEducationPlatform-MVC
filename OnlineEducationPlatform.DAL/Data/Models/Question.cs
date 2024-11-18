using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int Marks { get; set; }

        public QuestionType QuestionType { get; set; }

        [ForeignKey("Quiz")]
        public int? QuizId { get; set; }

        [ForeignKey("Exam")]
        public int? ExamId { get; set; }

        [ForeignKey("Course")]
        public int? CourseId { get; set; }

        public Exam Exam { get; set; }
        public Quiz Quiz { get; set; }
        public Course Course { get; set; }
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public ICollection<AnswerResult> AnswerResults { get; set; } = new HashSet<AnswerResult>();
    }
}

public enum QuestionType
{
    Multiple_Choice = 1,
    True_False = 2
}