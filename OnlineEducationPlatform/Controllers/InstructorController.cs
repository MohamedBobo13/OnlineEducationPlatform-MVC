using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Manager.InstructorManager;

namespace OnlineEducationPlatform.Controllers
{
    [Authorize(Roles = "Admin")]

    public class InstructorController : Controller
    {
        private readonly IInstructorManager _instructorManager;

        public InstructorController(IInstructorManager instructorManager)
        {
            _instructorManager = instructorManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var response = await _instructorManager.GetAllInstructorsAsync();
            return View("GetAll", response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmSoftDelete(string InstructorId)
        {
            var Instructor = await _instructorManager.GetinstructorByinstructortId(InstructorId);

            if (Instructor == null)
            {
                TempData["ErrorMessage"] = "Instructor not found.";
                return RedirectToAction("GetAll");
            }

            return View(Instructor); // Return the confirmation view with enrollment details
        }

        // POST: Soft delete enrollment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string InstructorId)
        {
            var result = await _instructorManager.softdeleteInstructor(InstructorId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Instructor successfully deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "Failed to delete the Instructor.";
            }

            return RedirectToAction("GetAll");
        }
    }
}
