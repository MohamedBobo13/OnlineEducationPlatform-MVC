using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.DAL.Data.Models;
using System.Threading.Tasks;

public interface IAnswerResultManager
{
    Task<List<AnswerResultReadVm>> GetAllAsync();
    Task<AnswerResultReadVm> GetByIdAsync(int id);
    Task AddAsync(AnswerResultAddVm answerResultAddVm);
    Task UpdateAsync(AnswerResultUpdateVm answerResultUpdateVm);
    Task DeleteAsync(AnswerResult answerResult);
    bool IdExist(int answerResultId);
}
