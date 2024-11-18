using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dtos
{
    public class QuestionReadVm
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Marks { get; set; }
        public QuestionType QuestionType { get; set; }
        public int QuizId { get; set; }
        public int ExamId { get; set; }
        public int CourseId { get; set; }
    }
}
