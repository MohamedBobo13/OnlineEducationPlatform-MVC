using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.QuizDto
{
    public class QuizUpdateVm
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalMarks { get; set; }
        public int LectureId { get; set; }
    }
}
