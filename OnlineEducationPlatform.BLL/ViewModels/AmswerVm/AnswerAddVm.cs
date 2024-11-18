using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineEducationPlatform.BLL.ViewModels.AmswerVm
{
    public class AnswerAddVm
    {
        [Required(ErrorMessage = "Answer text is required.")]
        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        [Required(ErrorMessage = "Question ID is required.")]
        [QuestionIdExists]
        public int QuestionId { get; set; }
    }

}
