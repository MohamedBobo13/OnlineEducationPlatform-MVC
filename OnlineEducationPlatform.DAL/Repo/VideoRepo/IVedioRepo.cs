using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repositories
{
    public interface IVedioRepo
    {
        Task AddAsync(Video video);
        Task<IEnumerable<Video>> GetAllAsync();
        Task<Video> GetByIdAsync(int id);
        Task UpdateAsync(Video video);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
