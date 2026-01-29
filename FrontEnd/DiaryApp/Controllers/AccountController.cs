using DiaryApp.Models.Auth;
using DiaryApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly DiaryApiService _diaryApiService;

        public AccountController(DiaryApiService diaryApiService)
        {
            _diaryApiService = diaryApiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _diaryApiService.LoginAsync(model.Email, model.Password);

            if (token == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }

            Response.Cookies.Append(
                "access_token",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

            return RedirectToAction("Diary", "DiaryEntries");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return RedirectToAction("Login");
        }

    }
}
