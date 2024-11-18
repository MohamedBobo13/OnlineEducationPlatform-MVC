using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.DAL.Repositories
{
	public class VedioRepo:IVedioRepo
    {
        private readonly EducationPlatformContext _context;

        public VedioRepo(EducationPlatformContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Video video)
        {
            await _context.Video.AddAsync(video);
            SaveChangesAsync();
        }

        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            var videos = await _context.Video.AsNoTracking().Where(v => v.IsDeleted == false).ToListAsync();
            if (videos != null)
            {
                return videos;
            }
            return null;
        }

        public async Task<Video> GetByIdAsync(int id)
        {

            return await _context.Video.Where(v => v.IsDeleted == false)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task UpdateAsync(Video video)
        {
            await SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var video = await _context.Video.FindAsync(id);
            if (video != null)
            {
                video.IsDeleted = true;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
