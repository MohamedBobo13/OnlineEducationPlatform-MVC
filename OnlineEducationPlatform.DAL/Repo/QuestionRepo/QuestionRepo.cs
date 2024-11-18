using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repo.QuestionRepo
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly EducationPlatformContext _context;

        public QuestionRepo(EducationPlatformContext context)
        {
            _context = context;
        }
        public IQueryable<Question> GetAll()
        {
            return  _context.Question.AsNoTracking();
        }
        public IQueryable<Question> GetExam()
        {
            return _context.Question.Where(q => q.ExamId != null);
        }

        public IQueryable<Question> GetQuiz()
        {
            return _context.Question.Where(q => q.QuizId != null);
        }

        public IQueryable<Question> GetCourseExam(int courseId)
        {
            return  _context.Question.Where(q => q.ExamId != null && q.CourseId == courseId);
            
        }

        public IQueryable<Question> GetCourseQuiz(int courseId)
        {
            return _context.Question.Where(q => q.QuizId != null && q.CourseId == courseId);
        }

        public Question GetById(int id)
        {
            return  _context.Question.FirstOrDefault(a => a.Id == id);
        }
        public void Add(Question question)
        {
             _context.Add(question);
            SaveChange();
        }
        public void Update(Question question)
        {
            _context.Update(question);
            SaveChange();
        }
        public void Delete(Question question)
        {
            question.IsDeleted = true;
            _context.Update(question);
            SaveChange();
        }
        public bool IdExist(int questionId)
        {
            return _context.Question.Any(q => q.Id == questionId); 
        }

       

        public void SaveChange()
        {
             _context.SaveChanges();
        }

        
    }
}