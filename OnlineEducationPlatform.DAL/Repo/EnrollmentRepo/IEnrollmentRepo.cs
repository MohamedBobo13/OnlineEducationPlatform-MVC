using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.EnrollmentRepo
{
    public interface IEnrollmentRepo
    {
        Task<bool> EnrollmentExistsAsyncbyid(int id);
        Task<bool> StudentHasEnrollmentsAsync(string studentid);
        Task<bool> AreAllenrollmentsSoftDeletedAsyncforstudent(string studentId);
        Task<bool> AreAllEnrollmentsSoftDeletedAsyncforcourse(int courseid);
        Task<bool> CourseHasEnrollmentsAsync(int courseId);
        Task<bool> IsEnrollmentSoftDeletedAsync(string studentId, int courseId);
        Task<bool> IsStudentSoftDeletedAsync(string studentId);
        Task<bool> IsCourseSoftDeletedAsync(int CourseId);
        Task<Enrollment> GetEnrollmentByIdIgnoreSoftDeleteAsync(int enrollmentId);

        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(string studentId);
        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);
        Task AddAsync(Enrollment enrollment);

        Task<bool> RemoveAsync(string studentId, int courseId);
        Task<bool> StudentExistsAsync(string studentId);
        Task<bool> CourseExistsAsync(int CourseId);
        Task<bool> EnrollmentExistsAsync(string studentId, int courseId);
        Task<bool> CompleteAsync();

        Task<Enrollment> GetEnrollmentByStudentAndCourseAsync(string studentId, int courseId);
        Task<Enrollment> GetEnrollmentByStudentAndCourseAsyncwithnosoftdeleted(string studentId, int courseId);
        Task<Enrollment> Getenrollmentbyid(int id);

        //Task UpdateEnrollmentAsync(Enrollment enrollment);
        Task HardDeleteEnrollmentAsync(Enrollment enrollment);
        Task<List<Enrollment>> GetAllEnrollmentsAsync();

        Task<List<Enrollment>> GetAllSoftDeletedEnrollmentsAsync();
        Task<bool> IsEnrollmentSoftDeletedAsyncbyid(int enrollmentid);
        Task UpdateEnrollmentAsync(Enrollment enrollment);
      


    }
}
