using OnlineEducationPlatform.BLL.Validation.StudentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dtos
{
    public class AnswerResultUpdateVm
    {
        [Required(ErrorMessage = "Answer Id is required.")]
        [AnswerResultIdExists]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Answer is required.")]
        public string StudentAnswer { get; set; }

        [Required(ErrorMessage = "MarksAwarded is required.")]
        public decimal MarksAwarded { get; set; }

        [Required(ErrorMessage = "Student ID is required.")]
        [StudentIdExists]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Question ID is required.")]
        [QuestionIdExists]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Answer Id is required.")]
        [AnswerIdExists]
        public int AnswerId { get; set; }
    }
}
