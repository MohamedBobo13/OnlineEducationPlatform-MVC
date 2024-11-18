using OnlineEducationPlatform.BLL.ViewModels.ExamResultDto;
using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;
using OnlineEducationPlatform.BLL.handleresponse;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.ExamResultmanager
{
    public interface IExamResultmanager
    {
        Task<ServiceResponse<List<Examresultreadvm>>> GetAllExamResults();

        Task<ServiceResponse<ExamResult>> GetExamResultAsync(string studentid, int examid);
        Task<ServiceResponse<bool>> softdeleteexamresult(string studentId, int examid);
        Task<ServiceResponse<bool>> updateexamresultbyid(updateexamresultVm examresultreaddto);
        Task<ServiceResponse<List<Examresultwithoutidvm>>> GetAllSoftDeletedexamresultsAsync();

        Task<ServiceResponse<bool>> HardDeleteExamresulttByStudentAndquizsync(string studentId, int examid);
        Task<ServiceResponse<Examresultwithoutidvm>> CreateexamresultAsync(Examresultwithoutidvm examresultwithoutiddto);

        Task<ServiceResponse<List<Examresultreadvm>>> GetStudentresultssByStudentIdAsync(string studentId);
        Task<ServiceResponse<List<Examresultreadvm>>> GetstudentresultsByExamIdAsync(int examid);
        Task<Examresultreadvm> GetexamresultByStudentAndexamId(string studentId, int examid);
        Task<Examresultreadvm> GetById(int id);
        Task<Examresultreadvm> GetExamResultByStudentAndExamIdHarddelete(string studentId, int examid);
        }
}
