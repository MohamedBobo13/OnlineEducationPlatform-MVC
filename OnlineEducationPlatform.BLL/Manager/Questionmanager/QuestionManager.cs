using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.BLL.Dto.QuestionDto;
using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.QuestionRepo;
using OnlineEducationPlatform.DAL.Repositories;

namespace OnlineEducationPlatform.BLL.Manager.Questionmanager
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IQuestionRepo _questionRepo;
        private readonly IMapper _mapper;

        public QuestionManager(IQuestionRepo questionRepo, IMapper mapper)
        {
            _questionRepo = questionRepo;
            _mapper = mapper;
        }
        public List<QuestionReadVm> GetAll()
        {
            var questions =  _questionRepo.GetAll().AsNoTracking().ToList();
            return _mapper.Map<List<QuestionReadVm>>(questions);
        }

        public List<QuestionCourseExamReadVm> GetExam()
        {
            var questionsCourseExam = _questionRepo.GetExam().ToList();
            if (questionsCourseExam == null)
            {
                return null;
            }
            return _mapper.Map<List<QuestionCourseExamReadVm>>(questionsCourseExam);
        }

        public List<QuestionCourseQuizReadVm> GetQuiz()
        {
            var questions = _questionRepo.GetQuiz().ToList();
            if (questions == null)
            {
                return null;
            }
            return _mapper.Map<List<QuestionCourseQuizReadVm>>(questions);
        }

        public List<QuestionCourseExamReadVm> GetCourseExam(int courseId)
        {
            var questionsCourseExam =  _questionRepo.GetCourseExam(courseId).ToList();
            if (questionsCourseExam == null)
            {
                return null;
            }
            return _mapper.Map<List<QuestionCourseExamReadVm>>(questionsCourseExam);
        }

        public List<QuestionCourseQuizReadVm> GetCourseQuiz(int courseId)
        {
            var questions = _questionRepo.GetCourseQuiz(courseId).ToList();
            if (questions == null)
            {
                return null;
            }
            return _mapper.Map<List<QuestionCourseQuizReadVm>>(questions);
        }


        public  QuestionReadVm GetById(int id)
        {
            var question =  _questionRepo.GetById(id);

            if (question == null)
                return null;

            return _mapper.Map<QuestionReadVm>(question);
        }
        public void AddExam(QuestionExamAddVm questionExamAddVm)
        {
             _questionRepo.Add(_mapper.Map<Question>(questionExamAddVm));
        }

        public void AddQuiz(QuestionQuizAddVm questionQuizAddVm)
        {
             _questionRepo.Add(_mapper.Map<Question>(questionQuizAddVm));
        }
        public void UpdateExam(QuestionExamUpdateVm questionExamUpdateVm)
        {
            var existingQuestionExam =  _questionRepo.GetById(questionExamUpdateVm.Id);
            if (existingQuestionExam == null)
            {
                return;
            }
            _questionRepo.Update(_mapper.Map(questionExamUpdateVm, existingQuestionExam));
        }

        public void UpdateQuiz(QuestionQuizUpdateVm questionQuizUpdateVm)
        {
            var existingQuestionQuiz =  _questionRepo.GetById(questionQuizUpdateVm.Id);
            if (existingQuestionQuiz == null)
            {
                return;
            }
            _questionRepo.Update(_mapper.Map(questionQuizUpdateVm, existingQuestionQuiz));
        }
        public void Delete(int id)
        {
            var questionModel =  _questionRepo.GetById(id);
            if (questionModel != null)
            {
                 _questionRepo.Delete(questionModel);
            }
        }
        public bool IdExist(int questionId)
        {
            return _questionRepo.IdExist(questionId); 
        }

        
    }
}