using AutoMapper;
using OnlineEducationPlatform.BLL.ViewModels.ExamDto;
using OnlineEducationPlatform.BLL.ViewModels.QuizDto;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.QuizRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.QuizManager
{
    public class QuizManager : IQuizManager
    {
        private readonly IQuizRepo _quizRepo;
        private readonly IMapper _mapper;

        public QuizManager(IQuizRepo quizRepo,IMapper mapper)
        {
            _quizRepo = quizRepo;
            _mapper = mapper;
        }
        public async Task<List<QuizReadVm>> GetAllAsync()
        {
            //Auto-Mapping
            var quizes=await _quizRepo.GetAll();
            return _mapper.Map<List<QuizReadVm>>(quizes);
        }

        public async Task<QuizReadVm> GetByIdAsync(int id)
        {
            var quiz = await _quizRepo.GetById(id);
            if (quiz is null)
            {
                return null;
            }
            //Auto-Mapping
            return _mapper.Map<QuizReadVm>(quiz);
        }

        public async Task<QuizAddVm> AddAsync(QuizAddVm quizAddDto)

        {


            var quiz=_mapper.Map<Quiz>(quizAddDto);
            
           

            await _quizRepo.Add(quiz);
            var saveresult = await _quizRepo.CompleteAsync();
            if (saveresult == true)
            {
                return new QuizAddVm
                {
                   
                    LectureId = quiz.LectureId,
                    CourseId = quiz.CourseId,
                    Title = quiz.Title,
                    TotalMarks = quiz.TotalMarks,
                    TotalQuestions = quiz.TotalQuestions,
                };

            }
            return null;
            //await _quizRepo.Add(_mapper.Map<Quiz>(quizAddDto));
        }


        public async Task<QuizUpdateVm> UpdateAsync(QuizUpdateVm quizUpdateDto)
        {
            var quiz = await _quizRepo.GetById(quizUpdateDto.Id);
            var existingquiz = await _quizRepo.quizExistsAsyncbyid(quizUpdateDto.Id);

            if (existingquiz == true)
            {
               
              
                quiz.Title =    quizUpdateDto.Title;

               
                quiz.TotalMarks = quizUpdateDto.TotalMarks;
                quiz.TotalQuestions = quizUpdateDto.TotalQuestions;
                //var updateexam= _mapper.Map<Exam>(examUpdateDto);

                await _quizRepo.Update(quiz);
                var saveresult = await _quizRepo.CompleteAsync();
                if (saveresult == true)
                {
                    return new QuizUpdateVm
                    {

                        Id = quiz.Id,
                        CourseId = quiz.CourseId,
                        LectureId=quiz.LectureId,
                        Title = quiz.Title,
                        TotalMarks = quiz.TotalMarks,
                        TotalQuestions = quiz.TotalQuestions,
                       
                    };
                }
                else
                {
                    return null;
                }


            }
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var quiz = await _quizRepo.GetById(id);
            if (quiz is null)
            {
                return;
            }
            await _quizRepo.Delete(quiz.Id);
        }
        public bool IdExist(int quizId)
        {
            return _quizRepo.IdExist(quizId);
        }
    }
}
