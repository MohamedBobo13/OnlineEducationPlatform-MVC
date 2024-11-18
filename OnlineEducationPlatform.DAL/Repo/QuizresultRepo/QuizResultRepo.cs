using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.QuizRepo
{
    public class QuizResultRepo : IQuizResultRepo
    {
        private readonly EducationPlatformContext _context;

        public QuizResultRepo(EducationPlatformContext Context)
        {
            _context = Context;
        }
        public async Task<List<QuizResult>> GetAllQuizResultsAsync()
        {
            return await _context.QuizResult.AsNoTracking().ToListAsync();
        }

        public async Task<QuizResult> GetQuizResultForStudentAsync(string studentId, int quizId)
        {

            return await _context.QuizResult
                .FirstOrDefaultAsync(qr => qr.StudentId == studentId && qr.QuizId == quizId);


        }
        public async Task<bool> quizExistsAsync(int QuizId)
        {
            return await _context.Quiz.IgnoreQueryFilters().AnyAsync(c => c.Id == QuizId);
        }
        public async Task<bool> quizresultExistsAsync(string studentId, int quizid)
        {
            return await _context.QuizResult.IgnoreQueryFilters()
                .AnyAsync(e => e.StudentId == studentId && e.QuizId == quizid);
        }
        public async Task<bool> quizresultExistsAsyncbyid(int id)
        {
            return await _context.QuizResult.IgnoreQueryFilters()
                .AnyAsync(e => e.Id==id);
        }
        public async Task<bool> StudentExistsAsync(string studentId)
        {
            //return await _context.User.AnyAsync(U => U.Id == studentId && U.UserType == TypeUser.Student);
            return await _context.User.IgnoreQueryFilters().AnyAsync(U => U.Id == studentId && U.UserType == TypeUser.Student);

        }
        public async Task<QuizResult> GetQuizResultByStudentAndQuizAsyncwithnosoftdeleted(string studentId, int quizid)
        {
            return await _context.QuizResult

                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.QuizId == quizid);


        }
        public async Task<bool> IsQuizResultSoftDeletedAsync(string studentId, int quizid)
        {
            return _context.QuizResult
                           .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                           .Any(e => e.StudentId == studentId
                                  && e.QuizId == quizid
                                  && e.IsDeleted == true);
        }
        public async Task<bool> RemoveAsync(string StudentId, int quizid)
        {
            var quizResult = await _context.QuizResult
        .FirstOrDefaultAsync(e => e.StudentId == StudentId && e.QuizId == quizid);
            if (quizResult != null)
            {
                quizResult.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
        public async Task<QuizResult> GetQuizResultsByIdIgnoreSoftDeleteAsync(int quizresultid )
        {
            return await _context.QuizResult
                .IgnoreQueryFilters() // This bypasses the global filter that excludes soft-deleted records
                .FirstOrDefaultAsync(e => e.Id == quizresultid);
        }

        public async Task<bool> IsQuizResultSoftDeletedAsyncbyid(int quizresultid)
        {
            return _context.QuizResult
                           .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                           .Any(e => e.Id == quizresultid

                                  && e.IsDeleted == true);
        }
        public async Task<bool> IsStudentSoftDeletedAsync(string studentId)
        {
            
            return await _context.Users
                         .IgnoreQueryFilters()  // Include soft-deleted records
                         .AnyAsync(u => u.Id == studentId
                         && u.IsDeleted == true
                                     && u.UserType == TypeUser.Student);

        }
        public async Task<bool> IsQuizSoftDeletedAsync(int quizid)
        {
            return _context.Quiz
                          .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                          .Any(e => e.Id == quizid

                                 && e.IsDeleted == true);


        }
        public async Task UpdateQuizResultAsync(QuizResult quizResult)
        {
            _context.QuizResult.Update(quizResult);
            await _context.SaveChangesAsync();


        }
        public async Task<List<QuizResult>> GetAllSoftDeletedQuizResultstsAsync()
        {
            return await _context.QuizResult
                                 .IgnoreQueryFilters().Where(e => e.IsDeleted)
                                 .ToListAsync();
        }
        public async Task<QuizResult> GetQuizresultByStudentAndQuizAsync(string studentId, int quizid)
        {
            // Disable the query filter to get soft-deleted enrollments
            return await _context.QuizResult
                                 .IgnoreQueryFilters() // Disable query filters
                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.QuizId == quizid);
        }
        public async Task HardDeleteQuizResultAsync(QuizResult quizResult)
        {
            _context.QuizResult.Remove(quizResult);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task AddAsync(QuizResult quizResult)
        {
            await _context.QuizResult.AddAsync(quizResult);
            // await _context.SaveChangesAsync();
        }
        public async Task<bool> StudentHasQuizresultAsync(string studentid)
        {
            return await _context.QuizResult.IgnoreQueryFilters()
                        .AnyAsync(e => e.StudentId == studentid);


        }
        public async Task<bool> AreAllQuizresultsSoftDeletedAsyncforstudent(string studentId)
        {
            // Get the total count of students
            //int totalStudents = await _context.Users.IgnoreQueryFilters()
            //                                  .CountAsync(u => u.UserType == TypeUser.Student);

            //// Get the count of students that are soft deleted
            //int softDeletedStudents = await _context.Users.IgnoreQueryFilters()
            //                                        .CountAsync(u => u.UserType == TypeUser.Student && u.IsDeleted);

            //// Check if the total count matches the soft deleted count
            //return totalStudents > 0 && totalStudents == softDeletedStudents;
            return await _context.QuizResult
        .IgnoreQueryFilters() // This ensures we are checking against all records, including soft deleted ones
        .Where(e => e.StudentId == studentId)
        .AllAsync(e => e.IsDeleted); // 
        }
        public async Task<IEnumerable<QuizResult>> GetByStudentIdAsync(string studentId)
        {
            return await _context.QuizResult
                .Where(e => e.StudentId == studentId)

                .ToListAsync();
        }
        public async Task<bool> QuizHasquizresultssAsync(int quizid)
        {
            return await _context.QuizResult.IgnoreQueryFilters()
                        .AnyAsync(e => e.QuizId == quizid);


        }
        public async Task<bool> AreAllQuizResultsSoftDeletedAsyncforquiz(int quizid)
        {
            return await _context.QuizResult.IgnoreQueryFilters().
        Where(e => e.QuizId == quizid)

                       .AllAsync(e => e.IsDeleted);

        }
        public async Task<IEnumerable<QuizResult>> GetByquizIdAsync(int quizid)
        {
            return await _context.QuizResult
                .Where(e => e.QuizId == quizid)

                .ToListAsync();
        }
        public async Task<bool> AreAllQuizResultsSoftDeletedAsync()
        {
            // Assuming you have a soft delete field such as `IsDeleted` or `DeletedAt`
            return !await _context.QuizResult.AnyAsync(qr => !qr.IsDeleted);
        }
        public async Task<QuizResult> Getequizresulttbyid(int id)
        {
            return await _context.QuizResult

                               .FirstOrDefaultAsync(e => e.Id == id);


        }
        public async Task<QuizResult> GetquizresultByStudentAndquizAsync(string studentId, int quizid)
        {
            // Disable the query filter to get soft-deleted enrollments
            return await _context.QuizResult
                                 .IgnoreQueryFilters() // Disable query filters
                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.QuizId == quizid);
        }

    }
}
