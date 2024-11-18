using OnlineEducationPlatform.BLL.ViewModels.QuizDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.QuizManager
{
    public interface IQuizManager
    {
        Task<List<QuizReadVm>> GetAllAsync();
        Task<QuizReadVm> GetByIdAsync(int id);
        Task<QuizAddVm> AddAsync(QuizAddVm quizAddDto);
        Task <QuizUpdateVm>UpdateAsync(QuizUpdateVm quizUpdateDto);
        Task DeleteAsync(int id);
        bool IdExist(int quizId);
    }
}
