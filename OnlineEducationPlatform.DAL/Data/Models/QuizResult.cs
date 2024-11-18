using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class QuizResult
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        public decimal Score { get; set; }
        public decimal TotalMarks { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }

        [ForeignKey("Student")]

        public string StudentId { get; set; }

        public Student student { get; set; }

        public Quiz Quiz { get; set; }
    }
}
