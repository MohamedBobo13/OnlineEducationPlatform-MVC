using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.DAL.Repo.AnswerResultRepo
{
	public class AnswerResultRepo : IAnswerResultRepo
	{
		private readonly EducationPlatformContext _context;

		public AnswerResultRepo(EducationPlatformContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<AnswerResult>> GetAllAsync()
		{
			return await _context.AnswerResult.AsNoTracking().ToListAsync();
		}
		public async Task<AnswerResult> GetByIdAsync(int id)
		{
			return await _context.AnswerResult.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
		}
		public async Task AddAsync(AnswerResult answerResult)
		{
			await _context.AddAsync(answerResult);
			await SaveChangeAsync();
		}
		public async Task UpdateAsync(AnswerResult answerResult)
		{
			_context.Update(answerResult);
			await SaveChangeAsync();
		}
		public async Task DeleteAsync(AnswerResult answerResult)
		{
			answerResult.IsDeleted = true; 
			_context.Update(answerResult);
			await SaveChangeAsync();
		}
		public bool IdExist(int answerResultId)
		{
			return _context.AnswerResult.Any(q => q.Id == answerResultId);
		}
		public async Task SaveChangeAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}