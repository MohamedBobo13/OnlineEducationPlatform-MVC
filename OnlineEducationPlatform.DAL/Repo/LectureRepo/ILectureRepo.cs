using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repositories
{
    public interface ILectureRepo
    {
        Task AddAsync(Lecture lecture);
        Task<IEnumerable<Lecture>> GetAllAsync();
        Task<Lecture> GetByIdAsync(int id);
        Task UpdateAsync(Lecture lecture);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
        public bool IdExist(int Lectureid);
        

    }
}
