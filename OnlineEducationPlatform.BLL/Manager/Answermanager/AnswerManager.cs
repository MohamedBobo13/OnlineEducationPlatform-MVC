using AutoMapper;
using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.BLL.Manager.Questionmanager;
using OnlineEducationPlatform.BLL.ViewModels.AmswerVm;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.AnswerRepo;
using OnlineEducationPlatform.DAL.Repositories;

namespace OnlineEducationPlatform.BLL.Manager.Answermanager
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepo _answerRepo;
        private readonly IMapper _mapper;
        private readonly IQuestionManager _questionManager;

        public AnswerManager(IAnswerRepo answerRepo, IMapper mapper,IQuestionManager questionManager)
        {
            _answerRepo = answerRepo;
            _mapper = mapper;
            _questionManager = questionManager;
        }

        public List<AnswerReadVm> GetAll()
        {
            var answers = _answerRepo.GetAll().Select(x => new AnswerReadVm
            {
                Id = x.Id,
                AnswerText = x.AnswerText,
                IsCorrect = x.IsCorrect,
                QuestionText = x.Question.Content
            }).ToList();
            return answers;
        }
        public  AnswerReadVm GetById(int id)
        {
            var answer = _answerRepo.GetById(id, includeQuestion: true); 

            if (answer == null)
                return null;

            return new AnswerReadVm
            {
                Id = answer.Id,
                AnswerText = answer.AnswerText,
                IsCorrect = answer.IsCorrect,
                QuestionText = answer.Question.Content 
            };
        }
        public void  Add(AnswerAddVm answerAddVm)
        {
             _answerRepo.Add(_mapper.Map<Answer>(answerAddVm));
        }
        public void  Update(AnswerUpdateVm answerUpdateVM)
        {
            var existingAnswer =  _answerRepo.GetById(answerUpdateVM.Id);
            if (existingAnswer == null)
            {
                return;
            }
            _answerRepo.Update(_mapper.Map(answerUpdateVM, existingAnswer));
        }
        public void  Delete(int id)
        {
            var AnswerModel =  _answerRepo.GetById(id);
            if (AnswerModel != null)
            {
                 _answerRepo.Delete(AnswerModel);
            }
        }
        public bool IdExist(int answerId)
        {
            return _answerRepo.IdExist(answerId);
        }
    }
}