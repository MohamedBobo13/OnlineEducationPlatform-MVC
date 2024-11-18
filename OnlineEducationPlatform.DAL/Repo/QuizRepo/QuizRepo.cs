using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.DAL.Repo.QuizRepo
{
	public class QuizRepo : IQuizRepo
    {
        private readonly EducationPlatformContext _context;

        public QuizRepo(EducationPlatformContext educationPlatformContext)
        {
            _context = educationPlatformContext;
        }
        public async Task Add(Quiz quiz)
        {
            await _context.Quiz.AddAsync(quiz);
         
        }

        public async Task<bool> Delete(int id)
        {
            var quiz = await _context.Quiz
                                        .FirstOrDefaultAsync(e => e.Id == id);
            if (quiz != null)
            {
                quiz.IsDeleted = true;
                _context.Update(quiz);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Quiz>> GetAll()
        {
            return await _context.Quiz.AsNoTracking().ToListAsync();

        }

        public async  Task<Quiz> GetById(int id)
        {
            return await _context.Quiz
                                         .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task Update(Quiz quiz)
        {
            _context.Quiz.Update(quiz);
            //await _context.SaveChangesAsync();
            //    await SaveChange();
        }
        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> quizExistsAsyncbyid(int id)
        {
            return await _context.Quiz
                .AnyAsync(e => e.Id == id);
        }
        public bool IdExist(int quizId)
        {
            return _context.Quiz.Any(q => q.Id == quizId);
        }
    }
}
