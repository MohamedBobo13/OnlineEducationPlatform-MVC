using AutoMapper;
using OnlineEducationPlatform.BLL.ViewModels.ExamResultDto;
using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;
using OnlineEducationPlatform.BLL.handleresponse;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.QuizRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.ExamResultmanager
{
    public class examresultmanager : IExamResultmanager
    {
        private readonly IExamResultRepo _examresult;

        private readonly IMapper _mapper;

        public examresultmanager(IExamResultRepo irepo, IMapper mapper)
        {
            _examresult = irepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Examresultwithoutidvm>> CreateexamresultAsync(Examresultwithoutidvm examresultwithoutiddto)
        {
            var examresult = new ExamResult
            {
                StudentId = examresultwithoutiddto.studentId,
                ExamId = examresultwithoutiddto.Examid,

            };

            var response = new ServiceResponse<Examresultwithoutidvm>();

            var student = await _examresult.StudentExistsAsync(examresult.StudentId);
            if (student == false)
            {
                response.Data = null;
                response.Message = $"Failed to save Exam result because Student with ID {examresult.StudentId} not found..";
                response.Success = false;

                return response;
                //  throw new KeyNotFoundException($"Student with ID {enrollment.StudentId} not found.");
            }
            var softdeletedstudent = await _examresult.IsStudentSoftDeletedAsync(examresult.StudentId);
            if (softdeletedstudent == true)
            {

                response.Data = null;
                response.Message = $"Failed to save Exam result because Student with  Id {examresult.StudentId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var exam = await _examresult.ExamExistsAsync(examresult.ExamId);
            if (exam == false)
            {
                response.Data = null;
                response.Message = $"Failed to save Exam result because Exam with ID {examresult.ExamId} not found..";
                response.Success = false;
                return response;
                //throw new KeyNotFoundException($"Student with ID {enrollment.CourseId} not found.");
            }
            var softdeeletdexam = await _examresult.IsexamSoftDeletedAsync(examresult.ExamId);
            if (softdeeletdexam == true)
            {

                response.Data = null;
                response.Message = $"Failed to save Exam result because Exam with  Id {examresult.ExamId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var existingexamresult = await _examresult.ExamresultExistsAsync(examresult.StudentId, examresult.ExamId);
            if (existingexamresult == true)
            {
                response.Data = null;
                response.Message = $"Failed to save Exam result because Student with ID {examresult.StudentId} in  Exam with ID {examresult.ExamId} already Exist.";
                response.Success = false;
                return response;
                // throw new InvalidOperationException($"Student {enrollment.StudentId} is already enrolled in course {enrollment.CourseId}.");
            }
            var softdeletedExamresult = await _examresult.IsexamResultSoftDeletedAsync(examresult.StudentId, examresult.ExamId);
            if (softdeletedExamresult == true)
            {
                response.Data = null;
                response.Message = $"Failed to save Exam result because Exam result is already exist but it is soft deleted.";
                response.Success = false;
                return response;


            }

            examresult.Score = examresultwithoutiddto.Score;
            examresult.TotalMarks = examresultwithoutiddto.TotalMarks;
            examresult.IsPassed = examresultwithoutiddto.IsPassed;
            await _examresult.AddAsync(examresult);
            var saveresult = await _examresult.CompleteAsync();
            if (saveresult)
            {
                response.Data = new Examresultwithoutidvm
                {
                    studentId = examresult.StudentId,
                    Examid = examresult.ExamId,
                    TotalMarks = examresult.TotalMarks,
                    Score = examresult.Score,
                    IsPassed = examresult.IsPassed,
                };

                response.Message = "Exam result added successfully.";

                response.Success = true;


            }
            return response;
        }

        public async Task<ServiceResponse<List<Examresultreadvm>>> GetAllExamResults()
        {
            var response = new ServiceResponse<List<Examresultreadvm>>();
            try
            {
                var ExamResults = await _examresult.GetAllExamResultsAsync();
                if (ExamResults == null || ExamResults.Any() == false)
                {
                    response.Message = "There Are No Exam Results yet !!";
                    response.Success = true;
                    return response;


                }
                var allexamresultssoftdeletes = await _examresult.AreAllExamResultsSoftDeletedAsync();
                if (allexamresultssoftdeletes == true)
                {
                    response.Success = false;
                    response.Message = "All Exam Results are soft deleted";
                    return response;
                }

                else
                {

                    // Map the domain entities to DTOs
                    response.Data = _mapper.Map<List<Examresultreadvm>>(ExamResults);
                    response.Message = "There Are Exam Results : ";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error fetching Exam Results: {ex.Message}";

            }
            return response;

        }

        public async Task<ServiceResponse<List<Examresultwithoutidvm>>> GetAllSoftDeletedexamresultsAsync()
        {
            var response = new ServiceResponse<List<Examresultwithoutidvm>>();
            try
            {
                var examresults = await _examresult.GetAllSoftDeletedexamResultstsAsync();

                if (examresults == null || examresults.Any() == false)
                {
                    response.Message = "There Are No Soft Deleted Exam results yet !!";
                    response.Success = true;



                }
                else
                {



                    response.Data = _mapper.Map<List<Examresultwithoutidvm>>(examresults);
                    response.Message = "There Are Soft Deleted Exam Results : ";
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

        public async Task<ServiceResponse<ExamResult>> GetExamResultAsync(string studentid, int examid)
        {
            var exams = new ExamResult
            {
                StudentId = studentid,
                ExamId = examid,

            };
            var response = new ServiceResponse<ExamResult>();
            var student = await _examresult.StudentExistsAsync(exams.StudentId);
            if (student == false)
            {
                response.Data = null;
                response.Message = $"Failed to Get Student Result because Student with ID {exams.StudentId} not found..";
                response.Success = false;

                return response;
                //  throw new KeyNotFoundException($"Student with ID {enrollment.StudentId} not found.");
            }
            var softdeletedstudent = await _examresult.IsStudentSoftDeletedAsync(exams.StudentId);
            if (softdeletedstudent == true)
            {

                response.Data = null;
                response.Message = $"Failed to Get Exam result because Student with  Id {exams.StudentId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var Exam = await _examresult.ExamExistsAsync(exams.ExamId);
            if (Exam == false)
            {
                response.Data = null;
                response.Message = $"Failed to Get Student Result because Exam with ID {exams.ExamId} not found..";
                response.Success = false;
                return response;
                //throw new KeyNotFoundException($"Student with ID {enrollment.CourseId} not found.");
            }
            var softdeeletexam = await _examresult.IsexamSoftDeletedAsync(exams.ExamId);
            if (softdeeletexam == true)
            {

                response.Data = null;
                response.Message = $"Failed to Get Exam result because Exam with  Id {exams.ExamId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }

            var existingexamresult = await _examresult.ExamresultExistsAsync(exams.StudentId, exams.ExamId);
            if (existingexamresult == false)
            {
                response.Data = null;
                response.Message = $"Failed to Get Student Result because Student with ID {exams.StudentId} in  Exam with ID {exams.ExamId} Is Not Existed.";
                response.Success = false;
                return response;
                // throw new InvalidOperationException($"Student {enrollment.StudentId} is already enrolled in course {enrollment.CourseId}.");
            }
            var softdeletedexamresult = await _examresult.IsexamResultSoftDeletedAsync(exams.StudentId, exams.ExamId);
            if (softdeletedexamresult == true)
            {
                response.Data = null;
                response.Message = $"Failed to Get Exam result because Exam result is already exist but it is soft deleted.";
                response.Success = false;
                return response;


            }
            else
            {
                var examresult = await _examresult.GetExamResultForStudentAsync(exams.StudentId, exams.ExamId);
                response.Data = new ExamResult
                {
                    Id = examresult.Id,
                    StudentId = examresult.StudentId,
                    ExamId = examresult.ExamId,
                    Score = examresult.Score,
                    TotalMarks = examresult.TotalMarks,
                    IsPassed = examresult.IsPassed,

                };
                response.Message = "Student Degree In Exam.";

                response.Success = true;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Examresultreadvm>>> GetstudentresultsByExamIdAsync(int examid)
        {
            var response = new ServiceResponse<List<Examresultreadvm>>();
            try
            {
                var Examexisted = await _examresult.ExamExistsAsync(examid);
                if (Examexisted == false)
                {
                    response.Message = $"Exam with ID {examid} does not exist.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }

                var issoftdeletedexam = await _examresult.IsexamSoftDeletedAsync(examid);
                if (issoftdeletedexam == true)
                {
                    response.Message = $"Exam with ID {examid} is soft deleted .";
                    response.Success = false;
                    response.Data = null;
                    return response;
                }
                var examhasexamresults = await _examresult.examHasexamresultssAsync(examid);
                if (examhasexamresults == false)
                {
                    response.Message = $"Exam with ID {examid} has no Exam results .";
                    response.Success = false;
                    response.Data = null;
                    return response;


                }
                var allExamresultsdeleted = await _examresult.AreAllexamResultsSoftDeletedAsyncforexam(examid);
                if (allExamresultsdeleted == true)
                {
                    response.Message = $"All Exam results in Exam with ID {examid} are soft deleted.";
                    response.Success = false;
                    response.Data = null;

                    return response;


                }

                var examresults = await _examresult.GetByexamIdAsync(examid);

                if (examresults.Any() == false || examresults == null)
                {
                    response.Message = $"Exam with ID {examid} does not have any Exam results.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }

                else
                {

                    response.Data = examresults.Select(e => new Examresultreadvm
                    {
                        id = e.Id,
                        studentId = e.StudentId,
                        Examid = e.ExamId,
                        Score = e.Score,
                        TotalMarks = e.TotalMarks,
                        IsPassed = e.IsPassed,

                    }).ToList();
                    response.Message = $"There are Exam results in the Exam.";
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

        public async Task<ServiceResponse<List<Examresultreadvm>>> GetStudentresultssByStudentIdAsync(string studentId)
        {
            var response = new ServiceResponse<List<Examresultreadvm>>();
            try
            {
                var Studentexist = await _examresult.StudentExistsAsync(studentId);
                if (Studentexist == false)
                {
                    response.Message = $"Student with ID {studentId} does not exist.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }
                var isstidentsoftdeleted = await _examresult.IsStudentSoftDeletedAsync(studentId);
                if (isstidentsoftdeleted == true)
                {
                    response.Message = $"Student with ID {studentId} is soft deleted .";
                    response.Success = false;
                    response.Data = null;
                    return response;
                }
                var studenthasexamresult = await _examresult.StudentHasexamresultAsync(studentId);
                if (studenthasexamresult == false)
                {
                    response.Message = $"Student with ID {studentId} has no Exam results .";
                    response.Success = false;
                    response.Data = null;
                    return response;


                }

                var allexamresultsdeletde = await _examresult.AreAllexamresultsSoftDeletedAsyncforstudent(studentId);
                if (allexamresultsdeletde == true)
                {
                    response.Message = $"All Exam results for Student with ID {studentId} are soft deleted.";
                    response.Success = false;
                    response.Data = null;

                    return response;


                }
                var examresults = await _examresult.GetByStudentIdAsync(studentId);
                if (examresults.Any() == false || examresults == null)
                {
                    response.Message = $"Student with ID {studentId} does not have any Exam results.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }
                else
                {

                    response.Data = examresults.Select(e => new Examresultreadvm
                    {
                        id = e.Id,
                        studentId = e.StudentId,
                        Examid = e.ExamId,
                        TotalMarks = e.TotalMarks,
                        Score = e.Score,
                        IsPassed = e.IsPassed,

                    }).ToList();
                    response.Message = $"There Are Exam results For The Student.";
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

        public async Task<ServiceResponse<bool>> HardDeleteExamresulttByStudentAndquizsync(string studentId, int examid)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var examresult = await _examresult.GetexamresultByStudentAndexamAsync(studentId, examid);

                if (examresult == null)
                {
                    response.Success = false;
                    response.Message = "Failed to Hard Delete Exam result because it is not existed !!";
                    return response;
                }

                if (examresult.IsDeleted)
                {
                    await _examresult.HardDeleteexamResultAsync(examresult);
                    response.Success = true;
                    response.Message = "Exam result Hard Deleted Successfully.";


                }
                else
                {
                    response.Success = false;
                    response.Message = "Exam result is not soft deleted. Please soft delete it before hard deletion.";

                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";

            }
            return response;
        }

        public async Task<ServiceResponse<bool>> softdeleteexamresult(string studentId, int examid)
        {
            var examresult = await _examresult.GetexamresultsByStudentAndexamAsyncwithnosoftdeleted(studentId, examid);

            var response = new ServiceResponse<bool>();
            try

            {

                var softdeletedexamresult = await _examresult.IsexamResultSoftDeletedAsync(studentId, examid);

                if (softdeletedexamresult == true)
                {

                    response.Message = $"Failed to soft delete Exam Result because Exam Result is already soft deleted ";
                    response.Success = false;
                    return response;


                }
                if (examresult == null)
                {
                    response.Success = false;
                    response.Message = "Failed to delete Exam Result because it is not existed !!";
                    return response;
                }






                var saveresult = await _examresult.RemoveAsync(examresult.StudentId, examresult.ExamId);




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

        public async Task<ServiceResponse<bool>> updateexamresultbyid(updateexamresultVm examresultreaddto)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var getexamresult = await _examresult.GetExamResultsByIdIgnoreSoftDeleteAsync(examresultreaddto.id);
                if (getexamresult == null)
                {
                    response.Success = false;
                    response.Message = $"Exam result with Id {examresultreaddto.id} not existed";
                    return response;
                }
                var sottdeletedexamresult = await _examresult.IsExamResultSoftDeletedAsyncbyid(examresultreaddto.id);
                if (sottdeletedexamresult == true)
                {
                    response.Success = false;
                    response.Message = $"Exam result with Id {examresultreaddto.id} is soft deleted";
                    return response;
                }






                getexamresult.Id = examresultreaddto.id;



                getexamresult.TotalMarks = examresultreaddto.TotalMarks;
                getexamresult.Score = examresultreaddto.Score;
                getexamresult.IsPassed = examresultreaddto.ispassed;


                await _examresult.UpdateExamResultAsync(getexamresult);
                response.Success = true;
                response.Message = "Exam result Updated Successfully";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
        public async Task<Examresultreadvm> GetexamresultByStudentAndexamId(string studentId, int examid)
        {
            var examresult = await _examresult.GetExamResultByStudentAndExamAsyncwithnosoftdeleted(studentId, examid);



            if (examresult == null)
            {
                return null;
            }

            // Assuming EnrollmentDtoForRetrieve is your DTO for displaying enrollment details
            return new Examresultreadvm
            {
                id = examresult.Id,
                studentId = examresult.StudentId,
                Examid = examresult.ExamId,
                TotalMarks = examresult.TotalMarks,
                Score = examresult.Score,
                IsPassed = examresult.IsPassed,
            };
        }
        public async Task<Examresultreadvm> GetById(int id)
        {
            var examresult = await _examresult.Getexamresulttbyid(id);

            if (examresult == null)
                return null;

            return new Examresultreadvm
            {
                id = examresult.Id,
                studentId = examresult.StudentId,
                Examid = examresult.ExamId,
                Score = examresult.Score,
                TotalMarks = examresult.TotalMarks,
                IsPassed = examresult.IsPassed,

            };
        }
        public async Task<Examresultreadvm> GetExamResultByStudentAndExamIdHarddelete(string studentId, int examid)
        {
            var examresult = await _examresult.GetexamresultByStudentAndexamAsync(studentId, examid);



            if (examresult == null)
            {
                return null;
            }

            // Assuming EnrollmentDtoForRetrieve is your DTO for displaying enrollment details
            return new Examresultreadvm
            {
                id = examresult.Id,
                studentId = examresult.StudentId,
                Examid = examresult.ExamId,
                Score = examresult.TotalMarks,
                IsPassed = examresult.IsPassed,
            };
        }
    }
}
