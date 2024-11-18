using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public ICollection<AnswerResult> AnswerResults { get; set; } = new HashSet<AnswerResult>();
    }
}
