using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.QuizRepo
{
    public interface IExamResultRepo
    {
        Task<bool> ExamresultExistsAsync(string studentId, int examid);

        Task<bool> ExamExistsAsync(int ExamId);
        Task<ExamResult> GetExamResultForStudentAsync(string studentId, int examid);
        Task<bool> StudentExistsAsync(string studentId);
        Task<List<ExamResult>> GetAllExamResultsAsync();
        Task<ExamResult> GetexamresultsByStudentAndexamAsyncwithnosoftdeleted(string studentId, int examid);
        Task<bool> IsexamResultSoftDeletedAsync(string studentId, int examid);
        Task<bool> RemoveAsync(string StudentId, int examid);


        Task<ExamResult> GetExamResultsByIdIgnoreSoftDeleteAsync(int ExamResult);
        Task<bool> IsExamResultSoftDeletedAsyncbyid(int Examresultid);
        Task<bool> IsStudentSoftDeletedAsync(string studentId);

        Task UpdateExamResultAsync(ExamResult examResult);
        Task<bool> IsexamSoftDeletedAsync(int examid);
        Task<bool> examresultExistsAsyncbyid(int id);
        Task<List<ExamResult>> GetAllSoftDeletedexamResultstsAsync();
        Task<ExamResult> GetexamresultByStudentAndexamAsync(string studentId, int examid);
        Task HardDeleteexamResultAsync(ExamResult examResult);
        Task<bool> CompleteAsync();
        Task AddAsync(ExamResult examResult);
        Task<bool> StudentHasexamresultAsync(string studentid);
        Task<bool> AreAllexamresultsSoftDeletedAsyncforstudent(string studentId);
        Task<IEnumerable<ExamResult>> GetByStudentIdAsync(string studentId);
        Task<bool> examHasexamresultssAsync(int examid);
        Task<bool> AreAllexamResultsSoftDeletedAsyncforexam(int examid);

        Task<IEnumerable<ExamResult>> GetByexamIdAsync(int examid);
        Task<bool> AreAllExamResultsSoftDeletedAsync();

        Task<ExamResult> GetExamResultByStudentAndExamAsyncwithnosoftdeleted(string studentId, int examid);
        Task<ExamResult> Getexamresulttbyid(int id);
      
       
    }
}
