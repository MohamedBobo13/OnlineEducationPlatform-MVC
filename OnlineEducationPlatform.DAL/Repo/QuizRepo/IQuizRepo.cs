using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.QuizRepo
{
    public interface IQuizRepo
    {

        Task<List<Quiz>> GetAll();
        Task<Quiz> GetById(int id);
        Task Add(Quiz quiz);
        Task Update(Quiz quiz);
        Task<bool> Delete(int id);
        Task<bool> CompleteAsync();
        Task<bool> quizExistsAsyncbyid(int id);
        bool IdExist(int quizId);

    }

}
