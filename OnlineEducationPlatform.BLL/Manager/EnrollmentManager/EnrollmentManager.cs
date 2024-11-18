using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto;
using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.BLL.handleresponse;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.EnrollmentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEducationPlatform.BLL.ViewModels.AmswerVm;

namespace OnlineEducationPlatform.BLL.Manager.EnrollmentManager
{
    public class EnrollmentManager : IenrollmentManager
    {
        private readonly IEnrollmentRepo _enrollmentRepository;
        private readonly IMapper _mapper;
        public EnrollmentManager(IEnrollmentRepo repo,IMapper mapper)
        {
            _enrollmentRepository = repo;
            _mapper= mapper;
        }

        public async Task  <ServiceResponse<List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>>> GetAllEnrollments()
        {
            var response = new ServiceResponse<List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>>();
            try
            {
                var enrollments = await _enrollmentRepository.GetAllEnrollmentsAsync();
                if (enrollments == null||enrollments.Any()==false)
                {
                    response.Message = "There Are No Enrollments yet !!";
                    response.Success=true;
                   
                       

                }
                else {

                    response.Data = _mapper.Map<List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>>(enrollments);
                    response.Message = "There Are Enrollments : ";
                    response.Success = true;
                }
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = $"Error fetching enrollments: {ex.Message}";

            }
            return response;

        }
     public async  Task<ServiceResponse<List<EnrollmentDtowWithStatusanddDate>>> GetAllSoftDeletedEnrollmentsAsync()
        {
            var response = new ServiceResponse<List<EnrollmentDtowWithStatusanddDate>>();
            try
            {
                var enrollments = await _enrollmentRepository.GetAllSoftDeletedEnrollmentsAsync();

                if (enrollments == null || enrollments.Any() == false)
                {
                    response.Message = "There Are No Soft Deleted Enrollments yet !!";
                    response.Success = true;



                }
                else
                {


                    foreach (var enrollment in enrollments)
                    {
                        enrollment.Status = EnrollmentStatus.removed;
                    }
                    response.Data = _mapper.Map<List<EnrollmentDtowWithStatusanddDate>>(enrollments);
                    response.Message = "There Are Soft Deleted Enrollments : ";
                    response.Success = true;
                }
            


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error fetching enrollments: {ex.Message}";

            }
            return response;


        }

        public async Task<ServiceResponse<EnrollmentDtowWithStatusanddDate>> CreateEnrollmentAsync(enrollmentvmwithdate enrollmentDto)
        {
            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId,
               EnrollmentDate=enrollmentDto.EnrollmentDate,
            };

            var response = new ServiceResponse<EnrollmentDtowWithStatusanddDate>();

            var student = await _enrollmentRepository.StudentExistsAsync(enrollment.StudentId);
            if (student == false)
            {
                response.Data = null;
                response.Message = $"Failed to save enrollment because Student with ID {enrollment.StudentId} not found..";
                response.Success = false;

                return response;
            }
            var softdeletedstudent = await _enrollmentRepository.IsStudentSoftDeletedAsync(enrollment.StudentId);
            if (softdeletedstudent==true) {

                response.Data = null;
                response.Message = $"Failed to save enrollment because Student with  Id {enrollment.StudentId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var Course = await _enrollmentRepository.CourseExistsAsync(enrollment.CourseId);
            if (Course == false)
            {
                response.Data = null;
                response.Message = $"Failed to save enrollment because Course with ID {enrollment.CourseId} not found..";
                response.Success = false;
                return response;
            }
            var softdeletedCourse = await _enrollmentRepository.IsCourseSoftDeletedAsync(enrollment.CourseId);
            if (softdeletedCourse == true)
            {

                response.Data = null;
                response.Message = $"Failed to save enrollment because Course with  Id {enrollment.CourseId} already exist but it is soft deleted.";
                response.Success = false;
                return response;
            }
            var softdeletedenrollment = await _enrollmentRepository.IsEnrollmentSoftDeletedAsync(enrollment.StudentId, enrollment.CourseId);
            if (softdeletedenrollment == true)
            {
                response.Data = null;
                response.Message = $"Failed to save enrollment because enrollment is already exist but it is soft deleted.";
                response.Success = false;
                return response;


            }
            var existingEnrollments = await _enrollmentRepository.EnrollmentExistsAsync(enrollment.StudentId, enrollment.CourseId);
            if (existingEnrollments == true)
            {
                response.Data = null;
                response.Message = $"Failed to save enrollment because Student with ID {enrollment.StudentId} in  Course with ID {enrollment.CourseId} already enrolled.";
                response.Success = false;
                return response;
            }
            
           // enrollment.EnrollmentDate = DateTime.Now;
            enrollment.Status = EnrollmentStatus.Enrolled;
            await _enrollmentRepository.AddAsync(enrollment);
            var saveresult = await _enrollmentRepository.CompleteAsync();
            if (saveresult)
            {
                response.Data = new EnrollmentDtowWithStatusanddDate
                {
                    StudentId = enrollment.StudentId,
                    CourseId = enrollment.CourseId,
                    EnrollmentDate = enrollment.EnrollmentDate,
                    Status = EnrollmentStatus.Enrolled.ToString(),
                };

                response.Message = "Enrollment added successfully.";

                response.Success = true;


            }
            return response;

        }

        public async Task<ServiceResponse<List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            var response = new ServiceResponse<List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>>();
            try
            {
                var Courseexists = await _enrollmentRepository.CourseExistsAsync(courseId);
                if (Courseexists == false)
                {
                    response.Message = $"Course with ID {courseId} does not exist.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }
               
                var issoftdeletedcourse = await _enrollmentRepository.IsCourseSoftDeletedAsync(courseId);
                if (issoftdeletedcourse == true) {
                    response.Message = $"Course with ID {courseId} is soft deleted .";
                    response.Success = false;
                    response.Data = null;
                    return response;
                }
                var coursehasenrollment = await _enrollmentRepository.CourseHasEnrollmentsAsync(courseId);
                if (coursehasenrollment == false)
                {
                    response.Message = $"Course with ID {courseId} has no enrollments .";
                    response.Success = false;
                    response.Data = null;
                    return response;


                }
                var allenrollmentdeleted = await _enrollmentRepository.AreAllEnrollmentsSoftDeletedAsyncforcourse(courseId);
                if (allenrollmentdeleted == true) {
                    response.Message = $"All Enrollments in Course with ID {courseId} are soft deleted.";
                    response.Success = false;
                    response.Data = null;

                    return response;


                }

                var enrollments = await _enrollmentRepository.GetByCourseIdAsync(courseId);

                if (enrollments.Any() == false || enrollments == null)
                {
                    response.Message = $"Course with ID {courseId} does not have any students.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }

                else
                {

                    response.Data = enrollments.Select(e => new EnrollmentDtoForRetriveAllEnrollmentsInCourse
                    {
                        Id = e.Id,
                        StudentId = e.StudentId,
                        CourseId = e.CourseId,
                        status = e.Status.ToString(),
                        EnrollmentDate = e.EnrollmentDate
                    }).ToList();
                    response.Message = $"There are enrollments in the course.";
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

        public async Task<ServiceResponse<List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>>> GetEnrollmentsByStudentIdAsync(string studentId)
        {
            var response = new ServiceResponse<List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>>();
            try
            {
                var Studentexist = await _enrollmentRepository.StudentExistsAsync(studentId);
                if (Studentexist == false)
                {
                    response.Message = $"Student with ID {studentId} does not exist.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }
                var isstidentsoftdeleted = await _enrollmentRepository.IsStudentSoftDeletedAsync(studentId);
                if (isstidentsoftdeleted == true)
                {
                    response.Message = $"Student with ID {studentId} is soft deleted .";
                    response.Success = false;
                    response.Data = null;
                    return response;
                }
                var studenthasenrollment = await _enrollmentRepository.StudentHasEnrollmentsAsync(studentId);
                if (studenthasenrollment == false)
                {
                    response.Message = $"Student with ID {studentId} has no enrollments .";
                    response.Success = false;
                    response.Data = null;
                    return response;


                }

                var allenrollmentdeleted = await _enrollmentRepository.AreAllenrollmentsSoftDeletedAsyncforstudent(studentId);
                if (allenrollmentdeleted == true)
                {
                    response.Message = $"All Enrollments for Student with ID {studentId} are soft deleted.";
                    response.Success = false;
                    response.Data = null;

                    return response;


                }






                var enrollments = await _enrollmentRepository.GetByStudentIdAsync(studentId);
                if (enrollments.Any() == false || enrollments == null)
                {
                    response.Message = $"Student with ID {studentId} does not have any Enrollments.";
                    response.Success = false;
                    response.Data = null;

                    return response;

                }
                else
                {

                    response.Data = enrollments.Select(e => new EnrollmentDtoForRetriveAllEnrollmentsInCourse
                    {
                        Id = e.Id,
                        StudentId = e.StudentId,
                        CourseId = e.CourseId,
                        status = e.Status.ToString(),
                        EnrollmentDate = e.EnrollmentDate
                    }).ToList();
                    response.Message = $"There Are Enrollments For The Student.";
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
       public async Task<ServiceResponse<bool>> HardDeleteEnrollmentByStudentAndCourseAsync(string studentId, int courseId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var enrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseAsync(studentId, courseId);

                if (enrollment == null)
                {
                    response.Success = false;
                    response.Message = "Failed to Hard Delete enrollment because it is not existed !!";
                    return response;
                }

                if (enrollment.IsDeleted)
                {
                    await _enrollmentRepository.HardDeleteEnrollmentAsync(enrollment);
                    response.Success = true;
                    response.Message = "Enrollment Hard Deleted Successfully.";


                }
                else
                {
                    response.Success = false;
                    response.Message = "Enrollment is not soft deleted. Please soft delete it before hard deletion.";

                }
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";

            }
            return response;

        }
        public async Task<ServiceResponse<bool>> UnenrollFromCourseByStudentAndCourseIdAsync(string StudentId,int CourseId)
        {
            
      
            var enrollment=await _enrollmentRepository.GetEnrollmentByStudentAndCourseAsyncwithnosoftdeleted(StudentId, CourseId);

            var response = new ServiceResponse<bool>();
            try




            {

                var softdeletedenrollment = await _enrollmentRepository.IsEnrollmentSoftDeletedAsync(StudentId, CourseId);

                if (softdeletedenrollment == true)
                {

                    response.Message = $"Failed to soft delete enrollment because enrollment is already soft deleted ";
                    response.Success = false;
                    return response;


                }
                if (enrollment == null)
                {
                    response.Success = false;
                    response.Message = "Failed to delete enrollment because it is not existed !!";
                    return response;
                }
              


            

                var saveresult = await _enrollmentRepository.RemoveAsync(enrollment.StudentId, enrollment.CourseId);

                if (saveresult)
                {
                   
                    response.Data = true;

                    response.Message = "Enrollment Soft deleted successfully.";

                    response.Success = true;


                }
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";

            }
            return response;


        }
        public async Task<ServiceResponse<bool>> updateenrollmentbyid(updateenrollmentVm updateenrollmentdto)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var getenollment = await _enrollmentRepository.GetEnrollmentByIdIgnoreSoftDeleteAsync(updateenrollmentdto.Id);
                if (getenollment == null)
                {
                    response.Success = false;
                    response.Message = $"Enrollment with Id {updateenrollmentdto.Id} not existed";
                    return response;
                }
                var softdeletedenollment = await _enrollmentRepository.IsEnrollmentSoftDeletedAsyncbyid(updateenrollmentdto.Id);
                if (softdeletedenollment == true)
                {
                    response.Success = false;
                    response.Message = $"Enrollment with Id {updateenrollmentdto.Id} is soft deleted";
                    return response;
                }
                var Studentexist = await _enrollmentRepository.StudentExistsAsync(updateenrollmentdto.StudentId);
                if (Studentexist == false)
                {
                    response.Message = $"Student with ID {updateenrollmentdto.StudentId} does not exist.";
                    response.Success = false;
                    

                    return response;

                }
                var isstudentsoftdeleted = await _enrollmentRepository.IsStudentSoftDeletedAsync(updateenrollmentdto.StudentId);
                if (isstudentsoftdeleted == true)
                {
                    response.Message = $"Student with ID {updateenrollmentdto.StudentId} is soft deleted .";
                    response.Success = false;
                    
                    return response;
                }
                var Courseexists = await _enrollmentRepository.CourseExistsAsync(updateenrollmentdto.CourseId);
                if (Courseexists == false)
                {
                    response.Message = $"Course with ID {updateenrollmentdto.CourseId} does not exist.";
                    response.Success = false;
                    

                    return response;

                }

                var issoftdeletedcourse = await _enrollmentRepository.IsCourseSoftDeletedAsync(updateenrollmentdto.CourseId);
                if (issoftdeletedcourse == true)
                {
                    response.Message = $"Course with ID {updateenrollmentdto.CourseId} is soft deleted .";
                    response.Success = false;
                   
                    return response;
                }
                var existingEnrollments = await _enrollmentRepository.EnrollmentExistsAsync(updateenrollmentdto.StudentId, updateenrollmentdto.CourseId);
                if (existingEnrollments == true)
                {

                    

                    getenollment.Id = updateenrollmentdto.Id;

                    getenollment.StudentId = updateenrollmentdto.StudentId;
                    getenollment.CourseId = updateenrollmentdto.CourseId;
                    getenollment.Status = updateenrollmentdto.Status;
                    getenollment.EnrollmentDate = updateenrollmentdto.EnrollmentDate;


                    await _enrollmentRepository.UpdateEnrollmentAsync(getenollment);
                    response.Success = true;
                    response.Message = "Enrollment Updated Successfully";
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
        public async Task<EnrollmentDtoForRetriveAllEnrollmentsInCourse> GetEnrollmentByStudentAndCourseId(string studentId, int courseId)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseAsyncwithnosoftdeleted(studentId, courseId);
            


            if (enrollment == null)
            {
                return null;
            }

            // Assuming EnrollmentDtoForRetrieve is your DTO for displaying enrollment details
            return new EnrollmentDtoForRetriveAllEnrollmentsInCourse
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                status = enrollment.Status.ToString(),
            };
        }
        public async Task<EnrollmentDtoForRetriveAllEnrollmentsInCourse> GetEnrollmentByStudentAndCourseIdHarddelete(string studentId, int courseId)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseAsync(studentId, courseId);



            if (enrollment == null)
            {
                return null;
            }

            // Assuming EnrollmentDtoForRetrieve is your DTO for displaying enrollment details
            return new EnrollmentDtoForRetriveAllEnrollmentsInCourse
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                status = enrollment.Status.ToString(),
            };
        }
        public async Task<EnrollmentDtoForRetriveAllEnrollmentsInCourse>  GetById(int id)
        {
            var enrollment = await _enrollmentRepository.Getenrollmentbyid(id);
            
            if (enrollment == null)
                return null;

            return new EnrollmentDtoForRetriveAllEnrollmentsInCourse
            {
                Id = enrollment.Id,
               StudentId=enrollment.StudentId,
               CourseId = enrollment.CourseId,
               status = enrollment.Status.ToString(),
               EnrollmentDate=enrollment.EnrollmentDate,

            };
        }


    }
}
