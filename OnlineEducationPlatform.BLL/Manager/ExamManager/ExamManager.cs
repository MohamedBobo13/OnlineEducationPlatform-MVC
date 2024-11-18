using AutoMapper;
using Azure;
using OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto;
using OnlineEducationPlatform.BLL.ViewModels.ExamDto;
using OnlineEducationPlatform.BLL.ViewModels.QuizDto;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.Iexamrepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.ExamManager
{
    public class ExamManager:IExamManager
    {
        private readonly Iexamrepo _examrepo;
        private readonly IMapper _mapper;

        public ExamManager(Iexamrepo examrepo,IMapper mapper)
        {
            _examrepo = examrepo;
            _mapper = mapper;
        }
        public async Task<List<ExamReadVm>> GetAllAsync()
        {
            //Auto-Mapping
            var exams = await _examrepo.GetAll();
            return _mapper.Map<List<ExamReadVm>>(exams);
        }

        public async Task<ExamReadVm> GetByIdAsync(int id)
        {
            var exam = await _examrepo.GetById(id);
            if (exam is null)
            {
                return null;
            }
            //Auto-Mapping
            return _mapper.Map<ExamReadVm>(exam);
        }

        public async Task<ExamAddVm> AddAsync(ExamAddVm examAddDto)

        {


            var exam = _mapper.Map<Exam>(examAddDto);



            await _examrepo.Add(exam);
            var saveresult = await _examrepo.CompleteAsync();
            if (saveresult == true)
            {
                return new ExamAddVm
                {

                   
                    CourseId = exam.CourseId,
                    Title = exam.Title,
                    TotalMarks = exam.TotalMarks,
                    TotalQuestions = exam.TotalQuestions,
                    DurationMinutes = exam.DurationMinutes,
                    PassingMarks = exam.PassingMarks,
                };

            }
            return null;
            //await _quizRepo.Add(_mapper.Map<Quiz>(quizAddDto));
        }


        public async Task<ExamUpdateVm> Update(ExamUpdateVm examUpdateDto)
        {
            var exam = await _examrepo.GetById(examUpdateDto.Id);
            var existingexam = await _examrepo.examExistsAsyncbyid(examUpdateDto.Id);
           
            if (existingexam == true)
            {
               
                exam.DurationMinutes = examUpdateDto.DurationMinutes;
                exam.CourseId = examUpdateDto.CourseId;
                exam.Title = examUpdateDto.Title;
                exam.PassingMarks = examUpdateDto.PassingMarks; 
                exam.TotalMarks = examUpdateDto.TotalMarks;
                exam.TotalQuestions = examUpdateDto.TotalQuestions;
               //var updateexam= _mapper.Map<Exam>(examUpdateDto);

                await _examrepo.Updateasync(exam);
                var saveresult = await _examrepo.CompleteAsync();
                if (saveresult == true)
                {
                    return new ExamUpdateVm
                    {


                        CourseId = exam.CourseId,
                        Title = exam.Title,
                        TotalMarks = exam.TotalMarks,
                        TotalQuestions = exam.TotalQuestions,
                        DurationMinutes = exam.DurationMinutes,
                        PassingMarks = exam.PassingMarks,
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
            var exam = await _examrepo.GetById(id);
            if (exam is null)
            {
                return;
            }
            await _examrepo.Delete(exam.Id);
     
        }
        public bool IdExist(int examId)
        {
            return _examrepo.IdExist(examId);
        }
    }
}
