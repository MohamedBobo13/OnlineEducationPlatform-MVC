using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Manager.StudentManager;

namespace OnlineEducationPlatform.Controllers
{
    [Authorize(Roles ="Admin")]

    public class StudentController : Controller
    {
        private readonly Istudentmanager _studentmanager;

        public StudentController(Istudentmanager studentmanager)
        {
            _studentmanager = studentmanager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var response = await _studentmanager.GetAllStudentsAsync();
            return View("GetAll", response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmSoftDelete(string studentId)
        {
            var Student = await _studentmanager.GetstudenttByStudentId(studentId);

            if (Student == null)
            {
                TempData["ErrorMessage"] = "Student not found.";
                return RedirectToAction("GetAll");
            }

            return View(Student); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string studentId)
        {
            var result = await _studentmanager.softdeleteStudent(studentId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Student successfully deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to delete the Student.";
            }

            return RedirectToAction("GetAll");
        }
    }
}
