using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.StudentRepo
{
    public class StudentRepo :IStudentRepo
    {
        private readonly EducationPlatformContext _context;

        public StudentRepo(EducationPlatformContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetAllStudents()
        {
            return await _context.Users.Where(u => u.UserType == TypeUser.Student).AsNoTracking().ToListAsync();
        }
        
        public async Task<bool> StudentExistsAsync(string studentId)
        {
            //return await _context.User.AnyAsync(U => U.Id == studentId && U.UserType == TypeUser.Student);
            return await _context.User.IgnoreQueryFilters().AnyAsync(U => U.Id == studentId && U.UserType == TypeUser.Student);

        }
        public async Task<bool> IsStudentSoftDeletedAsync(string studentId)
        {

            return await _context.Users
                         .IgnoreQueryFilters()  // Include soft-deleted records
                         .AnyAsync(u => u.Id == studentId
                         && u.IsDeleted == true
                                     && u.UserType == TypeUser.Student);

        }
        public async Task<ApplicationUser> GetStudentByIdAsync(string studentId)
        {
            // Assuming Role is an enum or a string where "Student" represents students
            return await _context.Users
                .Where(u => u.Id == studentId && u.UserType == TypeUser.Student && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> GetStudentByIdAsyncsoftornot(string studentId)
        {
            // Assuming Role is an enum or a string where "Student" represents students
            return await _context.Users.IgnoreQueryFilters()
                .Where(u => u.Id == studentId && u.UserType == TypeUser.Student && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }
       public async Task<ApplicationUser> GetStudentById(string studentId)
        {
            return await _context.Users
               .Where(u => u.Id == studentId && u.UserType == TypeUser.Student )
               .FirstOrDefaultAsync();

        }

        public async Task<bool> RemoveAsync(ApplicationUser student)
        {
           
            if (student != null)
            {
                student.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
        public bool IdExist(string studentId)
        {
            return _context.Student.Any(q => q.Id == studentId);
        }
    }
}
