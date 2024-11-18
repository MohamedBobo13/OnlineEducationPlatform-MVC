using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.DAL.Repositories
{
	public class LectureRepo : ILectureRepo
    {
        private readonly EducationPlatformContext _context;

        public LectureRepo(EducationPlatformContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Lecture lecture)
        {
            await _context.Lecture.AddAsync(lecture);
            SaveChangesAsync();
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            var lecture = await _context.Lecture.AsNoTracking().Where(l => l.IsDeleted == false).ToListAsync();
            if (lecture != null)
            {
                return lecture;
            }
            return null;
        }

        public async Task<Lecture> GetByIdAsync(int id)
        {

            return await _context.Lecture.Where(l => l.IsDeleted == false)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task UpdateAsync(Lecture lecture)
        {
            await SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lecture = await _context.Lecture.FindAsync(id);
            if (lecture != null)
            {
                lecture.IsDeleted = true;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public bool IdExist(int lectureid)
        {
            return _context.Lecture.Any(q => q.Id == lectureid);
        }

    }
}
