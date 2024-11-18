using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.InstructorRepo
{
    public interface IInstructorRepo
    {
        Task<List<ApplicationUser>> GetAllInstructors();
     
        Task<bool> InstructorExistsAsync(string InstructorId);

        Task<bool> IsInstructorSoftDeletedAsync(string InstructorId);
        Task<ApplicationUser> GetInstructorByIdAsync(string InstructorId);
        Task<ApplicationUser> GetInstructorByIdAsyncsoftornot(string InstructorId);
        Task<bool> RemoveAsync(ApplicationUser Instructor);
        Task<ApplicationUser> GetinstructorById(string InstructorId);
        public bool IdExist(string instrucorId);
        
    }
}
