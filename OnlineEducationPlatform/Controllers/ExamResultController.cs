using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Manager.ExamResultmanager;
using OnlineEducationPlatform.BLL.ViewModels.ExamResultDto;

namespace OnlineEducationPlatform.Controllers
{
    [Authorize(Roles = "Admin,Instructor,Student")]

    public class ExamResultController : Controller
    {
        private readonly IExamResultmanager _examResultmanager;

        public ExamResultController(IExamResultmanager examResultmanager)
        {
           _examResultmanager = examResultmanager;
        }
        [Authorize(Roles = "Admin,Instructor,Student")]

        public async Task<IActionResult> GetAllExamResultsForStudent(string studentId)
        {


            var response = await _examResultmanager.GetStudentresultssByStudentIdAsync(studentId);

            if (!response.Success)
            {
                // Handle the error, e.g., set an error message in TempData
                TempData["StudentErrorMessage"] = response.Message;
                return RedirectToAction("GetAll"); // Redirect to the list of enrollments
            }

            // Extract the data from the response and pass it to the view
            var quizresults = response.Data;
            ViewBag.StudentId = studentId;

            ViewBag.ErrorMessage = "Invalid StudentId Or No Exam Result found for the given StuedntId."; // This should be of type List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
            return View("GetExamResultByStudentId", quizresults); // Ensure your view expects IEnumerable<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> GetAllExamResultsForExam(int examid)
        {


            var response = await _examResultmanager.GetstudentresultsByExamIdAsync(examid);

            if (!response.Success)
            {
                // Handle the error, e.g., set an error message in TempData
                TempData["ExamErrorMessage"] = response.Message;
                return RedirectToAction("GetAll"); // Redirect to the list of enrollments
            }

            // Extract the data from the response and pass it to the view

            var ExamResult = response.Data;
            ViewBag.Examid = examid;

            ViewBag.ErrorMessage = "Invalid ExamId Or No Exam Results found for the given ExamId."; // This should be of type List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
            return View("GetExamResultsByExamId", ExamResult); // Ensure your view expects IEnumerable<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
        }
        [Authorize(Roles = "Admin,Instructor,Student")]

        public async Task<IActionResult> GetAll()
        {
            var response = await _examResultmanager.GetAllExamResults();
            return View("GetAll", response.Data);
        }
        [Authorize(Roles = "Admin,Instructor,Student")]

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Add()
        {

            return View();
        }
        // POST: Add Enrollment
        [ValidateAntiForgeryToken]

        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Add(Examresultwithoutidvm model)
        {


            if (ModelState.IsValid)
            {
                var response = await _examResultmanager.CreateexamresultAsync(model);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Exam Result successfully added!";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message ?? "Failed to create Exam Result. Please try again.";
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

        public async Task<IActionResult> ConfirmSoftDelete(string studentId, int examId)
        {
            var examresult = await _examResultmanager.GetexamresultByStudentAndexamId(studentId, examId);

            if (examresult == null)
            {
                TempData["ErrorMessage"] = "Exam Result not found.";
                return RedirectToAction("GetAll");
            }

            return View(examresult); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> SoftDelete(string studentId, int examId)
        {
            var result = await _examResultmanager.softdeleteexamresult(studentId, examId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Exam Result successfully soft deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to soft delete the Exam Result.";
            }

            return RedirectToAction("GetAll");
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Edit(int id)
        {
            var examresult = await _examResultmanager.GetById(id);
            if (examresult == null)
            {
                return NotFound();
            }

            var examresultUpdateVm = new updateexamresultVm
            {
                id = examresult.id,
                Score = examresult.Score,
                TotalMarks = examresult.TotalMarks,
                ispassed=examresult.IsPassed,

            };
            return View(examresultUpdateVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Edit(updateexamresultVm examresultupdate)
        {
            if (ModelState.IsValid)
            {
                var response = await _examResultmanager.updateexamresultbyid(examresultupdate);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Exam Result successfully Updated!";
                    return RedirectToAction("GetAll");  // Redirect to the enrollments list after success
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message ?? "Failed to Edit Exam Result. Please try again.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There are validation errors. Please correct them and try again.";
            }

            return View(examresultupdate);

        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllDeleted()
        {
            var response = await _examResultmanager.GetAllSoftDeletedexamresultsAsync();
            return View("GetAllDeleted", response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmHardDelete(string studentId, int examid)
        {
            var examresult = await _examResultmanager.GetExamResultByStudentAndExamIdHarddelete(studentId, examid);

            if (examresult == null)
            {
                TempData["ErrorMessage"] = "Exam Result not found.";
                return RedirectToAction("GetAllDeleted");
            }

            return View(examresult); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> HardDelete(string studentId, int examid)
        {
            var result = await _examResultmanager.HardDeleteExamresulttByStudentAndquizsync(studentId, examid);

            if (result.Success)
            {
                TempData["SuccessMessage"] = " Exam Result successfully Hard deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to Hard delete the Exam Result.";
            }

            return RedirectToAction("GetAllDeleted");
        }
    }
}
