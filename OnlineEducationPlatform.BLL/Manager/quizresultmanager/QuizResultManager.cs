using AutoMapper;
using OnlineEducationPlatform.BLL.ViewModels;
using OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto;
using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;
using OnlineEducationPlatform.BLL.handleresponse;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.EnrollmentRepo;
using OnlineEducationPlatform.DAL.Repo.QuizRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.quizresultmanager
{
    public class QuizResultManager : IQuizResultManager
    {
        private readonly IQuizResultRepo _quizresult;

        private readonly IMapper _mapper; 

        public QuizResultManager(IQuizResultRepo irepo, IMapper mapper)
        {
            _quizresult = irepo;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<quizresultwithoutidvm>> CreateQuizresultAsync(quizresultwithoutidvm quizresltadddto)
        {
            var quizresult = new QuizResult
            {
                StudentId = quizresltadddto.studentId,
                QuizId = quizresltadddto.QuizId,

            };

            var response = new ServiceResponse<quizresultwithoutidvm>();

            var student = await _quizresult.StudentExistsAsync(quizresult.StudentId);
            if (student == false)
            {
                response.Data = null;
                response.Message = $"Failed to save Quiz result because Student with ID {quizresult.StudentId} not found..";
                response.Success = false;

                return response;
                //  throw new KeyNotFoundException($"Student with ID {enrollment.StudentId} not found.");
            }
            var softdeletedstudent = await _quizresult.IsStudentSoftDeletedAsync(quizresult.StudentId);
            if (softdeletedstudent == true)
            {

                response.Data = null;
                response.Message = $"Failed to save Quiz result because Student with  Id {quizresult.StudentId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var quiz = await _quizresult.quizExistsAsync(quizresult.QuizId);
            if (quiz == false)
            {
                response.Data = null;
                response.Message = $"Failed to save Quiz result because Quiz with ID {quizresult.QuizId} not found..";
                response.Success = false;
                return response;
                //throw new KeyNotFoundException($"Student with ID {enrollment.CourseId} not found.");
            }
            var softdeeletdquiz = await _quizresult.IsQuizSoftDeletedAsync(quizresult.QuizId);
            if (softdeeletdquiz == true)
            {

                response.Data = null;
                response.Message = $"Failed to save Quiz result because Quiz with  Id {quizresult.QuizId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var existingquizresult = await _quizresult.quizresultExistsAsync(quizresult.StudentId, quizresult.QuizId);
            if (existingquizresult == true)
            {
                response.Data = null;
                response.Message = $"Failed to save Quiz result because Student with ID {quizresult.StudentId} in  Quiz with ID {quizresult.QuizId} already Exist.";
                response.Success = false;
                return response;
                // throw new InvalidOperationException($"Student {enrollment.StudentId} is already enrolled in course {enrollment.CourseId}.");
            }
            var softdeletedquizresult = await _quizresult.IsQuizResultSoftDeletedAsync(quizresult.StudentId, quizresult.QuizId);
            if (softdeletedquizresult == true)
            {
                response.Data = null;
                response.Message = $"Failed to save Quiz result because Quiz result is already exist but it is soft deleted.";
                response.Success = false;
                return response;


            }
         
            quizresult.Score=quizresltadddto.Score;
            quizresult.TotalMarks=quizresltadddto.TotalMarks;
           
            await _quizresult.AddAsync(quizresult);
            var saveresult = await _quizresult.CompleteAsync();
            if (saveresult)
            {
                response.Data = new quizresultwithoutidvm
                {
                    studentId = quizresult.StudentId,
                    QuizId = quizresult.QuizId,
                    TotalMarks = quizresult.TotalMarks,
                    Score = quizresult.Score,
                };

                response.Message = "Quiz result added successfully.";

                response.Success = true;


            }
            return response;

        }


        public async Task<ServiceResponse<List<quizresultreadvm>>> GetAllQuizResults()
        {
            
            
                var response = new ServiceResponse<List<quizresultreadvm>>();
                try
                {
                    var QuizResults = await _quizresult.GetAllQuizResultsAsync();
                    if (QuizResults == null || QuizResults.Any() == false)
                    {
                        response.Message = "There Are No QuizResults yet !!";
                        response.Success = true;

                    return response;

                    }
                var allquizresultssoftdeletes = await _quizresult.AreAllQuizResultsSoftDeletedAsync ();
                if (allquizresultssoftdeletes == true)
                {
                    response.Success = false;
                    response.Message = "All Quiz Results are soft deleted";
                    return response;
                }

                else
                {

                        // Map the domain entities to DTOs
                        response.Data = _mapper.Map<List<       quizresultreadvm>>(QuizResults);
                        response.Message = "There Are Quiz Results : ";
                        response.Success = true;
                    }
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = $"Error fetching Quiz Results: {ex.Message}";

                }
                return response;

            
        }

        public async Task<ServiceResponse<QuizResult>> GetQuizResultAsync(string studentid, int quizid)
        {

            var quizres = new QuizResult
            {
                StudentId = studentid,
                QuizId = quizid,

            };
            var response = new ServiceResponse<QuizResult>();
            var student = await _quizresult.StudentExistsAsync(quizres.StudentId);
            if (student == false)
            {
                response.Data = null;
                response.Message = $"Failed to Get Student Result because Student with ID {quizres.StudentId} not found..";
                response.Success = false;

                return response;
                //  throw new KeyNotFoundException($"Student with ID {enrollment.StudentId} not found.");
            }
            var softdeletedstudent = await _quizresult.IsStudentSoftDeletedAsync(quizres.StudentId);
            if (softdeletedstudent == true)
            {

                response.Data = null;
                response.Message = $"Failed to Get Quiz result because Student with  Id {quizres.StudentId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var Quiz = await _quizresult.quizExistsAsync(quizres.QuizId);
            if (Quiz == false)
            {
                response.Data = null;
                response.Message = $"Failed to Get Student Result because Quiz with ID {quizres.QuizId} not found..";
                response.Success = false;
                return response;
                //throw new KeyNotFoundException($"Student with ID {enrollment.CourseId} not found.");
            }
            var softdeeletdquiz = await _quizresult.IsQuizSoftDeletedAsync(quizres.QuizId);
            if (softdeeletdquiz == true)
            {

                response.Data = null;
                response.Message = $"Failed to Get Quiz result because Quiz with  Id {quizres.QuizId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }

            var existingquizresult = await _quizresult.quizresultExistsAsync(quizres.StudentId, quizres.QuizId);
            if (existingquizresult == false)
            {
                response.Data = null;
                response.Message = $"Failed to Get Student Result because Student with ID {quizres.StudentId} in  Quiz with ID {quizres.QuizId} Is Not Existed.";
                response.Success = false;
                return response;
                // throw new InvalidOperationException($"Student {enrollment.StudentId} is already enrolled in course {enrollment.CourseId}.");
            }
            var softdeletedquizresult = await _quizresult.IsQuizResultSoftDeletedAsync(quizres.StudentId, quizres.QuizId);
            if (softdeletedquizresult == true)
            {
                response.Data = null;
                response.Message = $"Failed to Get Quiz result because Quiz result is already exist but it is soft deleted.";
                response.Success = false;
                return response;


            }
            else
            {
                var quizresult = await _quizresult.GetQuizResultForStudentAsync(quizres.StudentId, quizres.QuizId);
                response.Data = new QuizResult
                {
                    Id = quizresult.Id,
                    StudentId = quizresult.StudentId,
                    QuizId = quizresult.QuizId,
                    Score = quizresult.Score,
                    TotalMarks = quizresult.TotalMarks,

                };
                response.Message = "Student Degree In Quiz.";

                response.Success = true;
                return response;
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> softdeletequizresult(string StudentId, int quizid)
        {
            //var enrollment = new Enrollment
            //{
            //    StudentId = enrollmentDto.StudentId,
            //    CourseId = enrollmentDto.CourseId,

            //};

            var quizresult = await _quizresult.GetQuizResultByStudentAndQuizAsyncwithnosoftdeleted(StudentId, quizid);

            var response = new ServiceResponse<bool>();
            try

            {

                var softdeletedquizresult = await _quizresult.IsQuizResultSoftDeletedAsync(StudentId, quizid);

                if (softdeletedquizresult == true)
                {

                    response.Message = $"Failed to soft delete Quiz Result because Quiz Result is already soft deleted ";
                    response.Success = false;
                    return response;


                }
                if (quizresult == null)
                {
                    response.Success = false;
                    response.Message = "Failed to delete Quiz REsult because it is not existed !!";
                    return response;
                }


                var saveresult = await _quizresult.RemoveAsync(quizresult.StudentId, quizresult.QuizId);
     
                if (saveresult)
                {
                  
                    response.Data = true;

                    response.Message = "Student Soft deleted successfully.";

                    response.Success = true;


                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";

            }
            return response;


        }
        public async Task<ServiceResponse<bool>> updatequizresultbyid(updatequizresultVm quizresultreaddto)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var getquizresult = await _quizresult.GetQuizResultsByIdIgnoreSoftDeleteAsync(quizresultreaddto.id);
                if (getquizresult == null)
                {
                    response.Success = false;
                    response.Message = $"Quiz result with Id {quizresultreaddto.id} not existed";
                    return response;
                }
                var softdeletedquizresult = await _quizresult.IsQuizResultSoftDeletedAsyncbyid(quizresultreaddto.id);
                if (softdeletedquizresult == true)
                {
                    response.Success = false;
                    response.Message = $"Quiz result with Id {quizresultreaddto.id} is soft deleted";
                    return response;
                }
              
                
                

                
              
                getquizresult.Id = quizresultreaddto.id;

              
            
                getquizresult.TotalMarks = quizresultreaddto.TotalMarks;
                getquizresult.Score = quizresultreaddto.Score;


                await _quizresult.UpdateQuizResultAsync(getquizresult);
                response.Success = true;
                response.Message = "Quiz result Updated Successfully";
               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
        public async Task<ServiceResponse<List<quizresultwithoutidvm>>> GetAllSoftDeletedQuizresultsAsync()
        {
            var response = new ServiceResponse<List<quizresultwithoutidvm>>();
            try
            {
                var quizresults = await _quizresult.GetAllSoftDeletedQuizResultstsAsync();

                if (quizresults == null || quizresults.Any() == false)
                {
                    response.Message = "There Are No Soft Deleted Quiz results yet !!";
                    response.Success = true;



                }
                else
                {


                    
                    response.Data = _mapper.Map<List<quizresultwithoutidvm>>(quizresults);
                    response.Message = "There Are Soft Deleted Quiz Results : ";
                    response.Success = true;
                }



            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error fetching Quiz Results: {ex.Message}";

            }
            return response;


        }
        public async Task<ServiceResponse<bool>> HardDeleteEQuizresulttByStudentAndquizsync(string studentId, int quizid)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var quizResult = await _quizresult.GetQuizresultByStudentAndQuizAsync(studentId, quizid);

                if (quizResult == null)
                {
                    response.Success = false;
                    response.Message = "Failed to Hard Delete Quiz result because it is not existed !!";
                    return response;
                }

                if (quizResult.IsDeleted)
                {
                    await _quizresult.HardDeleteQuizResultAsync(quizResult);
                    response.Success = true;
                    response.Message = "Quiz result Hard Deleted Successfully.";


                }
                else
                {
                    response.Success = false;
                    response.Message = "Quiz result is not soft deleted. Please soft delete it before hard deletion.";

                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";

            }
            return response;

        }
        public async Task<ServiceResponse<List<quizresultreadvm>>> GetStudentresultssByStudentIdAsync(string studentId)
        {
            var response = new ServiceResponse<List<quizresultreadvm>>();
            try
            {
                var Studentexist = await _quizresult.StudentExistsAsync(studentId);
                if (Studentexist == false)
                {
                    response.Message = $"Student with ID {studentId} does not exist.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }
                var isstidentsoftdeleted = await _quizresult.IsStudentSoftDeletedAsync(studentId);
                if (isstidentsoftdeleted == true)
                {
                    response.Message = $"Student with ID {studentId} is soft deleted .";
                    response.Success = false;
                    response.Data = null;
                    return response;
                }
                var studenthasquizresult = await _quizresult.StudentHasQuizresultAsync(studentId);
                if (studenthasquizresult == false)
                {
                    response.Message = $"Student with ID {studentId} has no Quiz results .";
                    response.Success = false;
                    response.Data = null;
                    return response;


                }

                var allquizresultsdeleted = await _quizresult.AreAllQuizresultsSoftDeletedAsyncforstudent(studentId);
                if (allquizresultsdeleted == true)
                {
                    response.Message = $"All Quiz results for Student with ID {studentId} are soft deleted.";
                    response.Success = false;
                    response.Data = null;

                    return response;


                }
                var quizresults = await _quizresult.GetByStudentIdAsync(studentId);
                if (quizresults.Any() == false || quizresults == null)
                {
                    response.Message = $"Student with ID {studentId} does not have any Quiz results.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }
                else
                {

                    response.Data = quizresults.Select(e => new quizresultreadvm
                    {
                        id = e.Id,
                        studentId = e.StudentId,
                        QuizId=e.QuizId,
                        TotalMarks=e.TotalMarks,
                        Score=e.Score,
                     
                    }).ToList();
                    response.Message = $"There Are Quiz results For The Student.";
                    response.Success = true;
                    return response;

                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";

            }
            return response;
        }
        public async Task<ServiceResponse<List<quizresultreadvm>>> GetstudentresultsByQuizIdAsync(int QuizId)
        {
            var response = new ServiceResponse<List<quizresultreadvm>>();
            try
            {
                var QuizExistd = await _quizresult.quizExistsAsync(QuizId);
                if (QuizExistd == false)
                {
                    response.Message = $"Quiz with ID {QuizId} does not exist.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }

                var issoftdeletedQuiz = await _quizresult.IsQuizSoftDeletedAsync(QuizId);
                if (issoftdeletedQuiz == true)
                {
                    response.Message = $"Quiz with ID {QuizId} is soft deleted .";
                    response.Success = false;
                    response.Data = null;
                    return response;
                }
                var quizhasquizresults = await _quizresult.QuizHasquizresultssAsync(QuizId);
                if (quizhasquizresults == false)
                {
                    response.Message = $"Quiz with ID {QuizId} has no Quiz results .";
                    response.Success = false;
                    response.Data = null;
                    return response;


                }
                var allQuizresultsdeleted = await _quizresult.AreAllQuizResultsSoftDeletedAsyncforquiz(QuizId);
                if (allQuizresultsdeleted == true)
                {
                    response.Message = $"All Quiz results in Quiz  with ID {QuizId} are soft deleted.";
                    response.Success = false;
                    response.Data = null;

                    return response;


                }

                var Quizresults = await _quizresult.GetByquizIdAsync(QuizId);

                if (Quizresults.Any() == false || Quizresults == null)
                {
                    response.Message = $"Quiz with ID {QuizId} does not have any Quiz results.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }

                else
                {

                    response.Data = Quizresults.Select(e => new quizresultreadvm
                    {
                        id=e.Id,
                        studentId=e.StudentId,
                        QuizId=e.QuizId,
                        Score=e.Score,
                        TotalMarks=e.TotalMarks,
                        
                    }).ToList();
                    response.Message = $"There are Quiz results in the Quiz.";
                    response.Success = true;
                    return response;

                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";

            }
            return response;
        }
        public async Task<quizresultreadvm> GetquizresultByStudentAndquizId(string studentId, int quizid)
        {
            var quizresult = await _quizresult.GetQuizResultByStudentAndQuizAsyncwithnosoftdeleted(studentId, quizid);



            if (quizresult == null)
            {
                return null;
            }

            // Assuming EnrollmentDtoForRetrieve is your DTO for displaying enrollment details
            return new quizresultreadvm
            {
                id = quizresult.Id,
                studentId = quizresult.StudentId,
                QuizId = quizresult.QuizId,
                TotalMarks = quizresult.TotalMarks,
                Score = quizresult.Score,
            };
        }
        public async Task<quizresultreadvm> GetById(int id)
        {
            var quizresult = await _quizresult.Getequizresulttbyid(id);

            if (quizresult == null)
                return null;

            return new quizresultreadvm
            {
                id = quizresult.Id,
                studentId = quizresult.StudentId,
                QuizId = quizresult.QuizId,
                Score = quizresult.Score,
                TotalMarks = quizresult.TotalMarks,

            };
        }
        public async Task<quizresultreadvm> GetQuizResultByStudentAndQuizIdHarddelete(string studentId, int quizid)
        {
            var quizresult = await _quizresult.GetquizresultByStudentAndquizAsync(studentId, quizid);



            if (quizresult == null)
            {
                return null;
            }

            // Assuming EnrollmentDtoForRetrieve is your DTO for displaying enrollment details
            return new quizresultreadvm
            {
                id = quizresult.Id,
                studentId = quizresult.StudentId,
                QuizId = quizresult.QuizId,
                Score = quizresult.Score,
                TotalMarks = quizresult.TotalMarks,
            };
        }
    }
}
