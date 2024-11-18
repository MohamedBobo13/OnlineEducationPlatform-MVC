using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.EnrollmentRepo
{
    public class EnrollmentRepo : IEnrollmentRepo
    {
        private readonly EducationPlatformContext _context;

        public EnrollmentRepo(EducationPlatformContext Context)
        {
            _context = Context;
        }

        public async Task<bool> CourseHasEnrollmentsAsync(int courseId)
        {
            return await _context.Enrollment.IgnoreQueryFilters()
                        .AnyAsync(e => e.CourseId == courseId);


        }
        public async Task<bool> StudentHasEnrollmentsAsync(string studentid)
        {
            return await _context.Enrollment.IgnoreQueryFilters()
                        .AnyAsync(e => e.StudentId == studentid);


        }
        public async Task<bool> AreAllEnrollmentsSoftDeletedAsyncforcourse(int courseid)
        {
            return await _context.Enrollment.IgnoreQueryFilters().Where(e=>e.CourseId==courseid)
                       .AllAsync(e => e.IsDeleted);

        }

        public async Task<bool> AreAllenrollmentsSoftDeletedAsyncforstudent(string studentId)
        {
            // Get the total count of students
            //int totalStudents = await _context.Users.IgnoreQueryFilters()
            //                                  .CountAsync(u => u.UserType == TypeUser.Student);

            //// Get the count of students that are soft deleted
            //int softDeletedStudents = await _context.Users.IgnoreQueryFilters()
            //                                        .CountAsync(u => u.UserType == TypeUser.Student && u.IsDeleted);

            //// Check if the total count matches the soft deleted count
            //return totalStudents > 0 && totalStudents == softDeletedStudents;
            return await _context.Enrollment
        .IgnoreQueryFilters() // This ensures we are checking against all records, including soft deleted ones
        .Where(e => e.StudentId == studentId)
        .AllAsync(e => e.IsDeleted); // 
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _context.Enrollment.AsNoTracking().ToListAsync();
        }
        public async Task<List<Enrollment>> GetAllSoftDeletedEnrollmentsAsync()
        {
            return await _context.Enrollment
                                 .IgnoreQueryFilters().Where(e => e.IsDeleted)
                                 .ToListAsync();
        }
        public async Task<bool> IsEnrollmentSoftDeletedAsync(string studentId, int courseId)
        {
            return _context.Enrollment
                           .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                           .Any(e => e.StudentId == studentId
                                  && e.CourseId == courseId
                                  && e.IsDeleted == true);
        }
        public async Task<bool> IsEnrollmentSoftDeletedAsyncbyid(int enrollmentid)
        {
            return _context.Enrollment
                           .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                           .Any(e => e.Id == enrollmentid

                                  && e.IsDeleted == true);
        }
        public async Task<bool> IsStudentSoftDeletedAsync(string studentId)
        {
            //return _context.Enrollment
            //              .IgnoreQueryFilters() // This tells EF to include soft-deleted records
            //              .Any(e => e.StudentId == studentId

            //                     && e.IsDeleted==true);
            return await _context.Users
                         .IgnoreQueryFilters()  // Include soft-deleted records
                         .AnyAsync(u => u.Id == studentId
                         && u.IsDeleted == true
                                     && u.UserType == TypeUser.Student);

        }
        public async Task<bool> IsCourseSoftDeletedAsync(int CourseId)
        {
            return _context.Course
                          .IgnoreQueryFilters() // This tells EF to include soft-deleted records
                          .Any(e => e.Id == CourseId

                                 && e.IsDeleted == true);


        }




        // Get enrollments by student ID
        public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(string studentId)
        {
            return await _context.Enrollment
                .Where(e => e.StudentId == studentId)

                .ToListAsync();
        }

        // Get enrollments by course ID
        public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Enrollment
                .Where(e => e.CourseId == courseId)

                .ToListAsync();
        }

        // Add new enrollment
        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollment.AddAsync(enrollment);
            // await _context.SaveChangesAsync();
        }




        public async Task<bool> StudentExistsAsync(string studentId)
        {
            return await _context.User.IgnoreQueryFilters().AnyAsync(U => U.Id == studentId && U.UserType == TypeUser.Student);
            // bool exists = await _context.Users
            //      .AnyAsync(u => u.UserId == studentId && u.Role == UserRole.Student);
        }

        public async Task<bool> CourseExistsAsync(int CourseId)
        {
            return await _context.Course.IgnoreQueryFilters().AnyAsync(c => c.Id == CourseId);
        }
        public async Task<bool> EnrollmentExistsAsync(string studentId, int courseId)
        {
            return await _context.Enrollment.IgnoreQueryFilters()
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
        public async Task<bool> EnrollmentExistsAsyncbyid(int id)
        {
            return await _context.Enrollment.IgnoreQueryFilters()
                .AnyAsync(e => e.Id == id);
        }
        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Enrollment> GetEnrollmentByIdIgnoreSoftDeleteAsync(int enrollmentId)
        {
            return await _context.Enrollment
                .IgnoreQueryFilters() // This bypasses the global filter that excludes soft-deleted records
                .FirstOrDefaultAsync(e => e.Id == enrollmentId);
        }

        //public async Task<bool> RemoveAsync(string StudentId, int CourseId)
        //{

        //    var enrollment = await _context.Enrollment
        //.FirstOrDefaultAsync(e => e.StudentId == StudentId && e.CourseId == CourseId);
        //    if (enrollment != null)
        //    {
        //        _context.Enrollment.Remove(enrollment);
        //        Save changes to persist the deletion
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;

        //}

        public async Task<bool> RemoveAsync(string StudentId, int CourseId)
        {
            var enrollment = await _context.Enrollment
        .FirstOrDefaultAsync(e => e.StudentId == StudentId && e.CourseId == CourseId);
            if (enrollment != null)
            {
                enrollment.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
        public async Task<Enrollment> GetEnrollmentByStudentAndCourseAsync(string studentId, int courseId)
        {
            // Disable the query filter to get soft-deleted enrollments
            return await _context.Enrollment
                                 .IgnoreQueryFilters() // Disable query filters
                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
        public async Task<Enrollment> GetEnrollmentByStudentAndCourseAsyncwithnosoftdeleted(string studentId, int courseId)
        {
            return await _context.Enrollment

                                 .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);


        }
        public async Task<Enrollment> Getenrollmentbyid(int id)
        {
            return await _context.Enrollment

                               .FirstOrDefaultAsync(e => e.Id==id);


        }
        public async Task HardDeleteEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        //{
        //    _context.Enrollment.Update(enrollment);
        //    await _context.SaveChangesAsync();
        //}
        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollment.Update(enrollment);
            await _context.SaveChangesAsync();
            

        }

    }
}
