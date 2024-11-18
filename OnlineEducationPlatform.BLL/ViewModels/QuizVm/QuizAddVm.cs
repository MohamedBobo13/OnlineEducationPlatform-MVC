using OnlineEducationPlatform.BLL.Validation.CourseValidation;
using OnlineEducationPlatform.BLL.Validation.LectureValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.QuizDto
{
    public class QuizAddVm
    {
        [CourseIdExists]

        public int CourseId { get; set; }
        public string Title { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalMarks { get; set; }
        [LectureExist]

        public int LectureId { get; set; }
    }
}
