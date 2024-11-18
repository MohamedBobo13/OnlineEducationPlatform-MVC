using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Manager.QuizManager;
using OnlineEducationPlatform.BLL.Manager.quizresultmanager;
using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;

namespace OnlineEducationPlatform.Controllers
{
    [Authorize(Roles = "Admin,Instructor,Student")]


    public class QuizResultController : Controller
    {
        private readonly IQuizResultManager _quizResultManager;

        public QuizResultController(IQuizResultManager quizResultManager)
        {
           
            _quizResultManager = quizResultManager;
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> GetAllQuizResultsForQuiz(int quizid)
        {


            var response = await _quizResultManager.GetstudentresultsByQuizIdAsync(quizid);

            if (!response.Success)
            {
                // Handle the error, e.g., set an error message in TempData
                TempData["QuizErrorMessage"] = response.Message;
                return RedirectToAction("GetAll"); // Redirect to the list of enrollments
            }

            // Extract the data from the response and pass it to the view

            var Quizresults = response.Data;
            ViewBag.QuizId = quizid;

            ViewBag.ErrorMessage = "Invalid QuizId or no Quiz Results found for the given QuizId."; // This should be of type List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
            return View("GetQuizResultsByQuizId", Quizresults); // Ensure your view expects IEnumerable<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
        }
        [Authorize(Roles = "Admin,Instructor,Student")]

        public async Task<IActionResult> GetAllQuizResultsForStudent(string studentId)
        {


            var response = await _quizResultManager.GetStudentresultssByStudentIdAsync(studentId);

            if (!response.Success)
            {
                // Handle the error, e.g., set an error message in TempData
                TempData["StudentErrorMessage"] = response.Message;
                return RedirectToAction("GetAll"); // Redirect to the list of enrollments
            }

            // Extract the data from the response and pass it to the view
            var quizresults = response.Data;
            ViewBag.StudentId = studentId;

            ViewBag.ErrorMessage = "Invalid StudentId or no Quiz Result found for the given StuedntId."; // This should be of type List<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
            return View("GetQuizResultByStudentId", quizresults); // Ensure your view expects IEnumerable<EnrollmentDtoForRetriveAllEnrollmentsInCourse>
        }
        [Authorize(Roles = "Admin,Instructor,Student")]

        public async Task<IActionResult> GetAll()
        {
            var response = await _quizResultManager.GetAllQuizResults();
            return View("GetAll", response.Data);
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

        public async Task<IActionResult> Add(quizresultwithoutidvm model)
        {


            if (ModelState.IsValid)
            {
                var response = await _quizResultManager.CreateQuizresultAsync(model);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Quiz Result successfully added!";
                    return RedirectToAction("GetAll");  
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message ?? "Failed to create Quiz Result. Please try again.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There are validation errors. Please correct them and try again.";
            }

            return View(model); // Return view with the form and error message if failed
        }
        [Authorize(Roles = "Admin,Instructor,Student")]


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> ConfirmSoftDelete(string studentId, int quizid)
        {
            var quizresult = await _quizResultManager.GetquizresultByStudentAndquizId(studentId, quizid);

            if (quizresult == null)
            {
                TempData["ErrorMessage"] = "Quiz Result not found.";
                return RedirectToAction("GetAll");
            }

            return View(quizresult); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> SoftDelete(string studentId, int QuizId)
        {
            var result = await _quizResultManager.softdeletequizresult(studentId, QuizId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "QuizResult successfully soft deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to soft delete the Quiz Result.";
            }

            return RedirectToAction("GetAll");
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Edit(int id)
        {
            var quizresult = await _quizResultManager.GetById(id);
            if (quizresult == null)
            {
                return NotFound();
            }

            var quizresultUpdateVm = new updatequizresultVm
            {
                id = quizresult.id,
                Score=quizresult.Score,
                TotalMarks=quizresult.TotalMarks,
                
            };
            return View(quizresultUpdateVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Edit(updatequizresultVm quizresultvm)
        {
            if (ModelState.IsValid)
            {
                var response = await _quizResultManager.updatequizresultbyid(quizresultvm);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Quiz Result successfully Updated!";
                    return RedirectToAction("GetAll");  // Redirect to the enrollments list after success
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message ?? "Failed to Edit Quiz Result. Please try again.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There are validation errors. Please correct them and try again.";
            }

            return View(quizresultvm);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllDeleted()
        {
            var response = await _quizResultManager.GetAllSoftDeletedQuizresultsAsync();
            return View("GetAllDeleted", response.Data);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> ConfirmHardDelete(string studentId, int quizId)
        {
            var quizresult = await _quizResultManager.GetQuizResultByStudentAndQuizIdHarddelete(studentId, quizId);

            if (quizresult == null)
            {
                TempData["ErrorMessage"] = "Quiz Result not found.";
                return RedirectToAction("GetAllDeleted");
            }

            return View(quizresult); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> HardDelete(string studentId, int quizid)
        {
            var result = await _quizResultManager.HardDeleteEQuizresulttByStudentAndquizsync(studentId, quizid);

            if (result.Success)
            {
                TempData["SuccessMessage"] = " Quiz Result successfully Hard deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to Hard delete the Quiz Result.";
            }

            return RedirectToAction("GetAllDeleted");
        }
    }
}
