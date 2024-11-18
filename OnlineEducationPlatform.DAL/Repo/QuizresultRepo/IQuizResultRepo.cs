using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.QuizRepo
{
    public interface IQuizResultRepo
    {
        Task<bool> quizresultExistsAsync(string studentId, int quizid);

        Task<bool> quizExistsAsync(int quizId);
        Task<QuizResult> GetQuizResultForStudentAsync(string studentId, int quizId);
        Task<bool> StudentExistsAsync(string studentId);
        Task<List<QuizResult>> GetAllQuizResultsAsync();
        Task<QuizResult> GetQuizResultByStudentAndQuizAsyncwithnosoftdeleted(string studentId, int quizid);
        Task<bool> IsQuizResultSoftDeletedAsync(string studentId, int quizid);
        Task<bool> RemoveAsync(string StudentId, int quizid);


        Task<QuizResult> GetQuizResultsByIdIgnoreSoftDeleteAsync(int QuizResult);
        Task<bool> IsQuizResultSoftDeletedAsyncbyid(int quizresultid);
        Task<bool> IsStudentSoftDeletedAsync(string studentId);

        Task UpdateQuizResultAsync(QuizResult quizResult);
        Task<bool> IsQuizSoftDeletedAsync(int Quizid);
        Task<bool> quizresultExistsAsyncbyid(int id);
        Task<List<QuizResult>> GetAllSoftDeletedQuizResultstsAsync();
         Task<QuizResult> GetQuizresultByStudentAndQuizAsync(string studentId, int quizid);
        Task HardDeleteQuizResultAsync(QuizResult quizResult);
        Task<bool> CompleteAsync();
        Task AddAsync(QuizResult quizResult);
        Task<bool> StudentHasQuizresultAsync(string studentid);
        Task<bool> AreAllQuizresultsSoftDeletedAsyncforstudent(string studentId);
        Task<IEnumerable<QuizResult>> GetByStudentIdAsync(string studentId);
        Task<bool> QuizHasquizresultssAsync(int quizid);
        Task<bool> AreAllQuizResultsSoftDeletedAsyncforquiz(int quizid);

        Task<IEnumerable<QuizResult>> GetByquizIdAsync(int quizid);
       Task<bool> AreAllQuizResultsSoftDeletedAsync();

         Task<QuizResult> Getequizresulttbyid(int id);
        Task<QuizResult> GetquizresultByStudentAndquizAsync(string studentId, int quizid);
       

    }
}
