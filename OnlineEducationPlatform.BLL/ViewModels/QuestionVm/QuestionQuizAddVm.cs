using OnlineEducationPlatform.BLL.Validation.CourseValidation;
using OnlineEducationPlatform.BLL.Validation.ExamValidation;
using OnlineEducationPlatform.BLL.Validation.QuizValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dtos
{
    public class QuestionQuizAddVm
    {
        [Required(ErrorMessage = "Course Id is required.")]
        [CourseIdExists]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Marks is required.")]
        public int Marks { get; set; }

        public QuestionType QuestionType { get; set; }

        [Required(ErrorMessage = "Quiz Id is required.")]
        [QuizIdExists]
        public int QuizId { get; set; }
    }
}
