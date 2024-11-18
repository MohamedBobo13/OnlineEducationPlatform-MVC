using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Manager.QuizManager;
using OnlineEducationPlatform.BLL.ViewModels.AmswerVm;
using OnlineEducationPlatform.BLL.ViewModels.QuizDto;
using OnlineEducationPlatform.DAL.Data.Models;
namespace OnlineEducationPlatform.Controllers
{
        [Authorize(Roles = "Admin,Instructor,Student")]

    public class QuizController : Controller
    {
        private readonly IQuizManager _quizManager;
        public QuizController(IQuizManager quizManager)
        {
            _quizManager = quizManager;
        }
        //[Authorize(Roles = "Student")]
        [Authorize(Roles = "Admin,Instructor,Student")]

        public async Task<IActionResult> Index()
        {
            var quiz = await _quizManager.GetAllAsync();
            return View(quiz);
        }
        [Authorize(Roles = "Admin,Instructor,Student")]


        public async Task<IActionResult> Details(int id)
        {
            var quiz = await _quizManager.GetByIdAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }
        [Authorize(Roles = "Admin,Instructor")]

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Create(QuizAddVm quizAddVm)
        {
            if (!ModelState.IsValid)
            {
                return View(quizAddVm);
            }
            await _quizManager.AddAsync(quizAddVm);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> Edit(int id)
        {
            var quiz = await _quizManager.GetByIdAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            var quizUpdateVm = new QuizUpdateVm
            {
                Id = quiz.Id,
                CourseId = quiz.CourseId,
                LectureId=quiz.LectureId,

               Title  = quiz.Title,
               TotalMarks = quiz.TotalMarks,
               TotalQuestions = quiz.TotalQuestions,

            };

            return View(quizUpdateVm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuizUpdateVm quizVm)
        {
            if (ModelState.IsValid)
            {
                await _quizManager.UpdateAsync(quizVm);
                return RedirectToAction(nameof(Index));
            }
            return View(quizVm);
        }
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> ConfirmSoftDelete(int id)
        {
            var Quiz = await _quizManager.GetByIdAsync(id);
            if (Quiz == null)
            {
                return NotFound();
            }

            return View(Quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public async Task<IActionResult> SoftDelete(int id)
        {
            await _quizManager.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
