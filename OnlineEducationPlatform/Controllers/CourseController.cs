using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Dto.CourseDto;
using OnlineEducationPlatform.BLL.Manager;

namespace OnlineEducationPlatform.Controllers
{
    [Authorize(Roles ="Admin,Instructor")]
	public class CourseController : Controller
	{
		private readonly ICourseManager _courseManager;

		public CourseController(ICourseManager courseManager)
        {
			_courseManager = courseManager;
		}
        [AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var courses =await _courseManager.GetAllAsync();
			return View(courses);
		}
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> Details(int id)
        {
            var course =await _courseManager.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                return View(course);
            }
        }
       

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
     

        public async Task<IActionResult> Create(CourseAddVm courseAddVm)
        {
            if (!ModelState.IsValid)
            {
                return View(courseAddVm);
            }
            await _courseManager.AddAsync(courseAddVm);
            return RedirectToAction("Index");
        }
      

        public async Task<IActionResult> Edit(int id)
        {
            var course =await _courseManager.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseUpdateVm = new CourseUpdateVm
            {
               Title= course.Title,
               Description= course.Description,
               TotalHours= course.TotalHours,
               CreatedDate=course.CreatedDate,
            };

            return View(courseUpdateVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public async Task<IActionResult> Edit(CourseUpdateVm courseUpdateVm)
        {
            if (ModelState.IsValid)
            {
                await _courseManager.UpdateAsync(courseUpdateVm);
                return RedirectToAction(nameof(Index));
            }
            return View(courseUpdateVm);
        }
       

        public async Task<IActionResult> Delete(int id)
        {
            var course =await _courseManager.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseManager.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
