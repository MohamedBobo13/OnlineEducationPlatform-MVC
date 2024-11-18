using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.StudentRepo
{
    public interface IStudentRepo
    {
       Task<List<ApplicationUser>> GetAllStudents();
       
        Task<bool> StudentExistsAsync(string studentId);

         Task<bool> IsStudentSoftDeletedAsync(string studentId);
        Task<ApplicationUser> GetStudentByIdAsync(string studentId);
        Task<ApplicationUser> GetStudentByIdAsyncsoftornot(string studentId);
        Task<bool> RemoveAsync(ApplicationUser student);
        Task<ApplicationUser> GetStudentById(string studentId);
        bool IdExist(string studentId);
    }
}
