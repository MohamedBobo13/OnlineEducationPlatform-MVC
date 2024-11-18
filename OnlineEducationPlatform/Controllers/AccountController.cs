using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Manager.Account_Manager;
using OnlineEducationPlatform.BLL.ViewModels.AcountVm;

namespace OnlineEducationPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVm loginDto, bool rememberMe)
        {
            var result = await _accountManager.LoginUser(loginDto, rememberMe);

            if (result == "Login successful")
            {
                return RedirectToAction("Index", "Home"); // Redirect to Home after successful login
            }

            ModelState.AddModelError("", result); // Display error message if login fails
            return View(loginDto); // Stay on the login page
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View("Register");
        }

        // POST: /account/register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterStudentVm registerDto)
        {
            var result = await _accountManager.RegisterStudent(registerDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Login"); // Redirect to login after successful registration
            }

            // Display validation errors if registration fails
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Register", registerDto); // Stay on the registration page
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountManager.LogoutUser();
            return RedirectToAction("Login"); // Redirect to login after logout
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterAdmin()
        {
            return View("RegisterAdmin");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin(RegesterAdminVm registerDto)
        {
            var result = await _accountManager.RegisterAdmin(registerDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Login"); // Redirect to login after successful registration
            }

            // Display validation errors if registration fails
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("RegisterAdmin", registerDto); // Stay on the registration page
        }

    }
}
