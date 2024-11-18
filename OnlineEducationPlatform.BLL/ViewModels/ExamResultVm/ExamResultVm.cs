using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto
{
    public class ExamResultVm
    {
        public string studentId { get; set; }

        public int Examid { get; set; }


    }
    public class Examresultwithoutidvm : ExamResultVm
    {
        
        public decimal Score { get; set; }
        public decimal TotalMarks { get; set; }
        public bool IsPassed { get; set; }

    }
    public class Examresultreadvm : Examresultwithoutidvm
	{

        public int id { get; set; }

        

    }
}
