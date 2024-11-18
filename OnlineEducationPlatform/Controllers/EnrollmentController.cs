using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto;
using OnlineEducationPlatform.BLL.Manager.EnrollmentManager;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace OnlineEducationPlatform.Controllers
{
    [Authorize(Roles = "Admin,Instructor,Student")]

    public class EnrollmentController : Controller
    {
        private readonly IenrollmentManager _enrollmentManager;

        public EnrollmentController(IenrollmentManager enrollmentManager)
        {
            _enrollmentManager = enrollmentManager;  
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> GetAllEnrollmentsForCourse(int courseId)
        {
           

            var response = await _enrollmentManager.GetEnrollmentsByCourseIdAsync(courseId);

            if (!response.Success)
            {
                // Handle the error, e.g., set an error message in TempData
                TempData["CourseErrorMessage"] = response.Message;
                return RedirectToAction("GetAll"); // Redirect to the list of enrollments
            }

            // Extract the data from the response and pass it to the view

            var enrollments = response.Data;
            ViewBag.CourseId = courseId;

            ViewBag.ErrorMessage = "Invalid CourseId or no enrollments found for the given CourseId."; // This should be of type List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
            return View("GetEnrollmentsByCourseId", enrollments); // Ensure your view expects IEnumerable<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
        }
        [Authorize(Roles = "Admin,Instructor,Student")]

        public async Task<IActionResult> GetAllEnrollmentsForStudent(string studentId)
        {


            var response = await _enrollmentManager.GetEnrollmentsByStudentIdAsync(studentId);

            if (!response.Success)
            {
                // Handle the error, e.g., set an error message in TempData
                TempData["StudentErrorMessage"] = response.Message;
                return RedirectToAction("GetAll"); // Redirect to the list of enrollments
            }

            // Extract the data from the response and pass it to the view
            var enrollments = response.Data;
            ViewBag.StudentId = studentId;  

            ViewBag.ErrorMessage = "Invalid StudentId or no enrollments found for the given StuedntId."; // This should be of type List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
            return View("GetEnrollmentsByStudentId", enrollments); // Ensure your view expects IEnumerable<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
        }

        [Authorize(Roles = "Admin,Instructor,Student")]

        public async Task <IActionResult> GetAll()
        {
            var response = await _enrollmentManager.GetAllEnrollments();
            return View("GetAll",response.Data);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllDeleted()
        {
            var response = await _enrollmentManager.GetAllSoftDeletedEnrollmentsAsync();
            return View("GetAllDeleted", response.Data);
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Add()
        {

            return View();
        }
        // POST: Add Enrollment
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        [HttpPost]
        public async Task<IActionResult> Add(enrollmentvmwithdate model)
        {

            
            if (ModelState.IsValid)
            {
                var response = await _enrollmentManager.CreateEnrollmentAsync(model);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Enrollment successfully added!";
                    return RedirectToAction("GetAll");  // Redirect to the enrollments list after success
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message ?? "Failed to create enrollment. Please try again.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There are validation errors. Please correct them and try again.";
            }

            return View(model); // Return view with the form and error message if failed
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> ConfirmSoftDelete(string studentId, int courseId)
        {
            var enrollment = await _enrollmentManager.GetEnrollmentByStudentAndCourseId(studentId, courseId);

            if (enrollment == null)
            {
                TempData["ErrorMessage"] = "Enrollment not found.";
                return RedirectToAction("GetAll");
            }

            return View(enrollment); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> SoftDelete(string studentId, int courseId)
        {
            var result = await _enrollmentManager.UnenrollFromCourseByStudentAndCourseIdAsync(studentId, courseId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Enrollment successfully soft deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to soft delete the enrollment.";
            }

            return RedirectToAction("GetAll");
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentManager.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentUpdateVm = new updateenrollmentVm
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                Status = (EnrollmentStatus)Enum.Parse(typeof(EnrollmentStatus), enrollment.status),
                EnrollmentDate = enrollment.EnrollmentDate,
            };
            return View(enrollmentUpdateVm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]

        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(updateenrollmentVm enrollmentvm)
        {
            if (ModelState.IsValid)
            {
                var response = await _enrollmentManager.updateenrollmentbyid(enrollmentvm);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Enrollment successfully Updated!";
                    return RedirectToAction("GetAll");  // Redirect to the enrollments list after success
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message ?? "Failed to Edit enrollment. Please try again.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There are validation errors. Please correct them and try again.";
            }

            return View(enrollmentvm);
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> ConfirmHardDelete(string studentId, int courseId)
        {
            var enrollment = await _enrollmentManager.GetEnrollmentByStudentAndCourseIdHarddelete(studentId, courseId);

            if (enrollment == null)
            {
                TempData["ErrorMessage"] = "Enrollment not found.";
                return RedirectToAction("GetAllDeleted");
            }

            return View(enrollment); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [Authorize(Roles = "Admin")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HardDelete(string studentId, int courseId)
        {
            var result = await _enrollmentManager.HardDeleteEnrollmentByStudentAndCourseAsync(studentId, courseId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Enrollment successfully Hard deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to Hard delete the enrollment.";
            }

            return RedirectToAction("GetAllDeleted");
        }
    }
}



