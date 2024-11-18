using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.DAL.Repositories
{
	public class CourseRepo : ICourseRepo
    {
        private readonly EducationPlatformContext _context;

        public CourseRepo(EducationPlatformContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Course course)
        {
            await _context.Course.AddAsync(course);
            SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses= await _context.Course.AsNoTracking().Where(c=>c.IsDeleted==false).ToListAsync();
            if (courses != null)
            {
                return courses;
            }
            return null;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            
            return await _context.Course.Where(c=>c.IsDeleted==false)
                .FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task UpdateAsync(Course course)
        {
            await SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course= await _context.Course.FindAsync(id);
            if (course != null)
            {
                course.IsDeleted = true;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public bool IdExist(int courseId)
        {
            return _context.Course.Any(q => q.Id == courseId);
        }

    }
}
