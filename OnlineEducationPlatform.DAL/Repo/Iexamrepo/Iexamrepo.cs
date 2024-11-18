using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.Iexamrepo
{
    public interface Iexamrepo
    {
        Task<List<Exam>> GetAll();
        Task<Exam> GetById(int id);
        Task Add(Exam exam);
        Task Updateasync(Exam exam);
        Task<bool> Delete(int id);
        Task<bool> CompleteAsync();
        Task<bool> examExistsAsyncbyid(int id);
        bool IdExist(int examId);
    }
}
