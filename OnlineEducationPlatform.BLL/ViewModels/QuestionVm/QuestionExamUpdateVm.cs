using OnlineEducationPlatform.BLL.Validation.CourseValidation;
using OnlineEducationPlatform.BLL.Validation.ExamValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dto.QuestionDto
{
    public class QuestionExamUpdateVm
    {
        [Required(ErrorMessage = "Question Id is required.")]
        [QuestionIdExists]
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Id is required.")]
        [CourseIdExists]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Marks is required.")]
        public int Marks { get; set; }

        public QuestionType QuestionType { get; set; }

        [Required(ErrorMessage = "Exam Id is required.")]
        [ExamIdExists]
        public int ExamId { get; set; }
    }
}
