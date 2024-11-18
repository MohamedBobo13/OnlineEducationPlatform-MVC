using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto
{
    public class QuizResultVm
    {
        public string studentId { get; set; }

        public int QuizId { get; set; }


    }
    public class quizresultwithoutidvm : QuizResultVm
    {

        public decimal Score { get; set; }
        public decimal TotalMarks { get; set; }
    }
    public class quizresultreadvm : quizresultwithoutidvm
	{

        public int id { get; set; }

        

    }
}
