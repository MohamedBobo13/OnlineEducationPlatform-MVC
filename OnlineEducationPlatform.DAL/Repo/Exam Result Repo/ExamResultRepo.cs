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
    public class ExamResultRepo : IExamResultRepo
    {
        private readonly EducationPlatformContext _context;

        public ExamResultRepo(EducationPlatformContext Context)
        {
            _context = Context;
        }
        public async Task<List<ExamResult>> GetAllExamResultsAsync()
        {
            return await _context.ExamResult.AsNoTracking().ToListAsync();
        }

        public async Task<ExamResult> GetExamResultForStudentAsync(string studentId, int examid)
        {

            return await _context.ExamResult
                .FirstOrDefaultAsync(qr => qr.StudentId == studentId && qr.ExamId == examid);


        }
        public async Task<bool> ExamExistsAsync(int examid)
        {
            return await _context.Exam.IgnoreQueryFilters().AnyAsync(c => c.Id == examid);
        }
        public async Task<bool> ExamresultExistsAsync(string studentId, int examid)
        {
            return await _context.ExamResult.IgnoreQueryFilters()
                .AnyAsync(e => e.StudentId == studentId && e.ExamId == examid);
        }
        public async Task<bool> examresultExistsAsyncbyid(int id)
        {
            return await _context.ExamResult.IgnoreQueryFilters()
                .AnyAsync(e => e.Id == id);
        }
        public async Task<bool> StudentExistsAsync(string studentId)
        {
            //return await _context.User.AnyAsync(U => U.Id == studentId && U.UserType == TypeUser.Student);
            return await _context.User.IgnoreQueryFilters().AnyAsync(U => U.Id == studentId && U.UserType == TypeUser.Student);

        }
        public async Task<ExamResult> GetexamresultsByStudentAndexamAsyncwithnosoftdeleted(string studentId, int examid)
        {
            return await _context.ExamResult

                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ExamId == examid);


        }
        public async Task<bool> IsexamResultSoftDeletedAsync(string studentId, int examid)
        {
            return _context.ExamResult
                           .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                           .Any(e => e.StudentId == studentId
                                  && e.ExamId ==examid
                                  && e.IsDeleted == true);
        }
        public async Task<bool> RemoveAsync(string StudentId, int examid)
        {
            var quizResult = await _context.ExamResult
        .FirstOrDefaultAsync(e => e.StudentId == StudentId && e.ExamId == examid);
            if (quizResult != null)
            {
                quizResult.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
        public async Task<ExamResult> GetExamResultsByIdIgnoreSoftDeleteAsync(int examresultid)
        {
            return await _context.ExamResult
                .IgnoreQueryFilters() // This bypasses the global filter that excludes soft-deleted records
                .FirstOrDefaultAsync(e => e.Id == examresultid);
        }

        public async Task<bool> IsExamResultSoftDeletedAsyncbyid(int examresultid)
        {
            return _context.ExamResult
                           .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                           .Any(e => e.Id == examresultid

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
        public async Task<bool> IsexamSoftDeletedAsync(int examid)
        {
            return _context.Exam
                          .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                          .Any(e => e.Id == examid

                                 && e.IsDeleted == true);


        }
        public async Task UpdateExamResultAsync(ExamResult examResult)
        {
            _context.ExamResult.Update(examResult);
            await _context.SaveChangesAsync();


        }
        public async Task<List<ExamResult>> GetAllSoftDeletedexamResultstsAsync()
        {
            return await _context.ExamResult
                                 .IgnoreQueryFilters().Where(e => e.IsDeleted)
                                 .ToListAsync();
        }
        public async Task<ExamResult> GetexamresultByStudentAndexamAsync(string studentId, int examid)
        {
            // Disable the query filter to get soft-deleted enrollments
            return await _context.ExamResult
                                 .IgnoreQueryFilters() // Disable query filters
                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ExamId == examid);
        }
       
        public async Task HardDeleteexamResultAsync(ExamResult examResult)
        {
            _context.ExamResult.Remove(examResult);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task AddAsync(ExamResult examResult)
        {
            await _context.ExamResult.AddAsync(examResult);
            // await _context.SaveChangesAsync();
        }
        public async Task<bool> StudentHasexamresultAsync(string studentid)
        {
            return await _context.ExamResult.IgnoreQueryFilters()
                        .AnyAsync(e => e.StudentId == studentid);


        }
        public async Task<bool> AreAllexamresultsSoftDeletedAsyncforstudent(string studentId)
        {
            // Get the total count of students
            //int totalStudents = await _context.Users.IgnoreQueryFilters()
            //                                  .CountAsync(u => u.UserType == TypeUser.Student);

            //// Get the count of students that are soft deleted
            //int softDeletedStudents = await _context.Users.IgnoreQueryFilters()
            //                                        .CountAsync(u => u.UserType == TypeUser.Student && u.IsDeleted);

            //// Check if the total count matches the soft deleted count
            //return totalStudents > 0 && totalStudents == softDeletedStudents;
            return await _context.ExamResult
        .IgnoreQueryFilters() // This ensures we are checking against all records, including soft deleted ones
        .Where(e => e.StudentId == studentId)
        .AllAsync(e => e.IsDeleted); // 
        }
        public async Task<IEnumerable<ExamResult>> GetByStudentIdAsync(string studentId)
        {
            return await _context.ExamResult
                .Where(e => e.StudentId == studentId)

                .ToListAsync();
        }
        public async Task<bool> examHasexamresultssAsync(int examid)
        {
            return await _context.ExamResult.IgnoreQueryFilters()
                        .AnyAsync(e => e.ExamId == examid);


        }
        public async Task<bool> AreAllexamResultsSoftDeletedAsyncforexam(int examid)
        {
            return await _context.ExamResult.IgnoreQueryFilters().
        Where(e => e.ExamId == examid)

                       .AllAsync(e => e.IsDeleted);

        }
        public async Task<IEnumerable<ExamResult>> GetByexamIdAsync(int examid)
        {
            return await _context.ExamResult
                .Where(e => e.ExamId == examid)

                .ToListAsync();
        }
        public async Task<bool> AreAllExamResultsSoftDeletedAsync()
        {
            // Assuming you have a soft delete field such as `IsDeleted` or `DeletedAt`
            return !await _context.ExamResult.AnyAsync(qr => !qr.IsDeleted);
        }
        public async Task<ExamResult> GetExamResultByStudentAndExamAsyncwithnosoftdeleted(string studentId, int examid)
        {
            return await _context.ExamResult

                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ExamId == examid);


        }
        public async Task<ExamResult> Getexamresulttbyid(int id)
        {
            return await _context.ExamResult

                               .FirstOrDefaultAsync(e => e.Id == id);






        }








        }
    }
