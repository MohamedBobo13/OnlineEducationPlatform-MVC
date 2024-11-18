using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.QuestionRepo
{
    public interface IQuestionRepo
    {
        IQueryable<Question> GetAll();
        IQueryable<Question> GetExam();
        IQueryable<Question> GetQuiz();
        IQueryable<Question> GetCourseExam(int courseId);
        IQueryable<Question> GetCourseQuiz(int courseId);
        Question GetById(int id);
        void Delete(Question question);
        void Update(Question question);
        void Add(Question question);
        bool IdExist(int questionId);
        void SaveChange();
    }
}
