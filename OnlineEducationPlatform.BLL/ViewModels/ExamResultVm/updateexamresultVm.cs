using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.ExamResultDto
{
    public class updateexamresultVm
    {
        public int id { get; set; }
        public decimal Score { get; set; }
        public decimal TotalMarks { get; set; }
        public bool ispassed { get; set; }
    }
}
