using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.DAL.Repo.AnswerRepo
{
    public class AnswerRepo : IAnswerRepo
    {
        private readonly EducationPlatformContext _context;

        public AnswerRepo(EducationPlatformContext context)
        {
            _context = context;
        }
        public  IQueryable<Answer> GetAll()
        {
            return  _context.Answer.AsNoTracking();
        }
        public  Answer GetById(int id, bool includeQuestion = false)
        {
            if (includeQuestion)
            {
                return _context.Answer
                    .Include(a => a.Question) 
                    .FirstOrDefault(a => a.Id == id);
            }
            else
            {
                return _context.Answer.Find(id);
            }
        }
        public void  Add(Answer answer)
        {
             _context.Add(answer);
             _context.SaveChanges();
        }
        public void Update(Answer answer)
        {
            _context.Update(answer);
            _context.SaveChanges();
        }
        public void  Delete(Answer answer)
        {
            answer.IsDeleted = true;
            _context.Update(answer);
            _context.SaveChanges();
        }
        public bool IdExist(int answerId)
        {
            return _context.Answer.Any(q => q.Id == answerId);
        }
        
        public void SaveChange()
        {
             _context.SaveChanges();
        }

        
    }
}