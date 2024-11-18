using OnlineEducationPlatform.BLL.ViewModels.ExamDto;
using OnlineEducationPlatform.BLL.ViewModels.QuizDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.ExamManager
{
    public interface IExamManager
    {
        Task<List<ExamReadVm>> GetAllAsync();
        Task<ExamReadVm> GetByIdAsync(int id);
        Task<ExamAddVm> AddAsync(ExamAddVm examAddDto);
        Task <ExamUpdateVm> Update(ExamUpdateVm examUpdateDto);
        Task DeleteAsync(int id);
        bool IdExist(int examId);
    }
}
