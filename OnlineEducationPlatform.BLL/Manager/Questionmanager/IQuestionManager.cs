using OnlineEducationPlatform.BLL.Dto.QuestionDto;
using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.BLL.Manager.Questionmanager
{
    public interface IQuestionManager
    {
        List<QuestionReadVm> GetAll();
        
        List<QuestionCourseExamReadVm> GetExam();

        List<QuestionCourseQuizReadVm> GetQuiz();

        List<QuestionCourseExamReadVm> GetCourseExam(int courseId);

        List<QuestionCourseQuizReadVm> GetCourseQuiz(int courseId);

        QuestionReadVm GetById(int id);

        void AddQuiz(QuestionQuizAddVm questionQuizAddVm);

        void AddExam(QuestionExamAddVm questionExamAddVm);

        void UpdateExam(QuestionExamUpdateVm questionExamUpdateVm);

        void UpdateQuiz(QuestionQuizUpdateVm questionQuizUpdateVm);

        void Delete(int id);

        bool IdExist(int questionId);

    }
}