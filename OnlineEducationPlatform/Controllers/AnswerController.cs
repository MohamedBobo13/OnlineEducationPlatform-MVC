using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEducationPlatform.BLL.Manager.Answermanager;
using OnlineEducationPlatform.BLL.Manager.Questionmanager;
using OnlineEducationPlatform.BLL.ViewModels.AmswerVm;

namespace OnlineEducationPlatform.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]

    public class AnswerController : Controller
    {
        private readonly IAnswerManager _answerManager;
        private readonly IQuestionManager _questionManager;

        public AnswerController(IAnswerManager answerManager,IQuestionManager questionManager)
        {
            _answerManager = answerManager;
            _questionManager = questionManager;
        }
        [Authorize(Roles = "Admin,Instructor")]

        public IActionResult Index()
        {
            var answer = _answerManager.GetAll();
             return View(answer);
        }
        [Authorize(Roles = "Admin,Instructor")]

        public IActionResult Details(int id)
        {
            var answer = _answerManager.GetById(id); 

            if (answer == null)
            {
                return NotFound(); 
            }

            var viewModel = new AnswerDetailsVm
            {
                Id = answer.Id,
                AnswerText = answer.AnswerText,
                IsCorrect = answer.IsCorrect,
                QuestionText = answer.QuestionText 
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Admin,Instructor")]

        public IActionResult Create()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]

        public IActionResult Create(AnswerAddVm answerAddVm)
        {
            if (!ModelState.IsValid)
            {
                return View(answerAddVm);
            }
            _answerManager.Add(answerAddVm);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Instructor")]

        public IActionResult Edit(int id)
        {
            var answer = _answerManager.GetById(id);
            if (answer == null)
            {
                return NotFound();
            }

            var answerUpdateVm = new AnswerUpdateVm
            {
                Id = answer.Id,
                AnswerText = answer.AnswerText,
                IsCorrect = answer.IsCorrect,
                QuestionId = answer.QuestionId
            };

            return View(answerUpdateVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]

        public IActionResult Edit(AnswerUpdateVm answerVm)
        {
            if (ModelState.IsValid)
            {
                _answerManager.Update(answerVm);
                return RedirectToAction(nameof(Index));
            }
            return View(answerVm);
        }
        [Authorize(Roles = "Admin,Instructor")]

        public IActionResult Delete(int id)
        {
            var answer = _answerManager.GetById(id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]

        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _answerManager.Delete(id);

            return RedirectToAction("Index");
        }
        
    }
}
