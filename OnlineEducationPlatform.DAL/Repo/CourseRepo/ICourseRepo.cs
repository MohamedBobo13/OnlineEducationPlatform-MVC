using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repositories
{
    public interface ICourseRepo
    {
        Task AddAsync(Course course);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task UpdateAsync(Course course);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
        bool IdExist(int courseId);

    }
}
