using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dtos
{
    public class AnswerUpdateVm
    {
        [Required(ErrorMessage = "Answer ID is required.")]
        [AnswerIdExists]
        public int Id { get; set; }

        [Required(ErrorMessage = "Answer text is required.")]
        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        [Required(ErrorMessage = "Question ID is required.")]
        [QuestionIdExists]
        public int QuestionId { get; set; }
    }
}
