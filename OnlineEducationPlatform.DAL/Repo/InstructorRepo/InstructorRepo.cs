using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.InstructorRepo
{
    public class InstructorRepo:IInstructorRepo
    {
        private readonly EducationPlatformContext _context;

        public InstructorRepo(EducationPlatformContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> GetinstructorById(string InsturctorId)
        {
            return await _context.Users
               .Where(u => u.Id == InsturctorId && u.UserType == TypeUser.Instructor)
               .FirstOrDefaultAsync();

        }



        public async Task<List<ApplicationUser>> GetAllInstructors()
        {
            return await _context.Users.Where(u => u.UserType == TypeUser.Instructor).AsNoTracking().ToListAsync();

        }

        public async Task<ApplicationUser> GetInstructorByIdAsync(string InstructorId)
        {
            return await _context.Users
                .Where(u => u.Id == InstructorId && u.UserType == TypeUser.Instructor && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetInstructorByIdAsyncsoftornot(string InstructorId)
        {
            return await _context.Users.IgnoreQueryFilters()
                .Where(u => u.Id == InstructorId && u.UserType == TypeUser.Instructor && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> InstructorExistsAsync(string InstructorId)
        {
            return await _context.User.IgnoreQueryFilters().AnyAsync(U => U.Id == InstructorId && U.UserType == TypeUser.Instructor);
        }

        public async Task<bool> IsInstructorSoftDeletedAsync(string InstructorId)
        {
            return await _context.Users
                        .IgnoreQueryFilters()  // Include soft-deleted records
                        .AnyAsync(u => u.Id == InstructorId
                        && u.IsDeleted == true
                                    && u.UserType == TypeUser.Instructor);
        }

        public async Task<bool> RemoveAsync(ApplicationUser Instructor)
        {
            if (Instructor != null)
            {
                Instructor.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
        public bool IdExist(string instructorid)
        {
            return _context.Instructor.Any(q => q.Id == instructorid);
        }
    }
}
