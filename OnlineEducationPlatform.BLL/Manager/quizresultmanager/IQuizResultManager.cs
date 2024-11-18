using OnlineEducationPlatform.BLL.ViewModels;
using OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto;
using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;
using OnlineEducationPlatform.BLL.handleresponse;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.quizresultmanager
{
    public interface IQuizResultManager
    {
        Task<ServiceResponse<List<quizresultreadvm>>> GetAllQuizResults();

        Task<ServiceResponse<QuizResult>> GetQuizResultAsync(string studentid, int quizid);
        Task<ServiceResponse<bool>> softdeletequizresult(string studentId, int quizid);
        Task<ServiceResponse<bool>> updatequizresultbyid(updatequizresultVm quizresultreaddto);
        Task<ServiceResponse<List<quizresultwithoutidvm>>> GetAllSoftDeletedQuizresultsAsync();

        Task<ServiceResponse<bool>> HardDeleteEQuizresulttByStudentAndquizsync(string studentId, int quizid);
       Task<ServiceResponse<quizresultwithoutidvm>> CreateQuizresultAsync(quizresultwithoutidvm quizresultwithoutiddto);

       Task<ServiceResponse<List<quizresultreadvm>>> GetStudentresultssByStudentIdAsync(string studentId);
        Task<ServiceResponse<List<quizresultreadvm>>> GetstudentresultsByQuizIdAsync(int QuizId);

        Task<quizresultreadvm> GetquizresultByStudentAndquizId(string studentId, int quizid);
       Task<quizresultreadvm>  GetById(int id);
        Task<quizresultreadvm> GetQuizResultByStudentAndQuizIdHarddelete(string studentId, int quizid);



    }
}
