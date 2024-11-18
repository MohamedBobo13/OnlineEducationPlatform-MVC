using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.BLL.ViewModels.AmswerVm;

namespace OnlineEducationPlatform.BLL.Manager.Answermanager
{
    public interface IAnswerManager
    {
        List<AnswerReadVm> GetAll();

        AnswerReadVm GetById(int id);

        void Add(AnswerAddVm answerAddVm);

        void Update(AnswerUpdateVm answerUpdateVm);

        void Delete(int id);

        bool IdExist(int answerId);
    }
}