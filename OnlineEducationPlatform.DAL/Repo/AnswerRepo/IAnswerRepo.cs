using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.AnswerRepo
{
    public interface IAnswerRepo
    {
        IQueryable<Answer> GetAll();
        Answer GetById(int id, bool includeQuestion = false);
        void Delete(Answer answer);
        void Update(Answer answer);
        void Add(Answer answer);
        bool IdExist(int answerId);
        void SaveChange();
    }
}
