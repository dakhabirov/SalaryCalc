using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalaryCalculator.Models;
using SalaryCalculator.ViewModels;

namespace SalaryCalculator.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async System.Threading.Tasks.Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)    // проверяем введенные данные на валидность
            {
                var user = await _userManager.FindByNameAsync(model.UserName);  // ищем пользователя по введенному логину

                // если пользователь найден
                if (user != null)
                {
                    // проверяем введенный пароль
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                    else
                        ModelState.AddModelError("", "Неверное имя пользователя или пароль. Пожалуйста, проверьте введенные данные.");
                }
                else
                    ModelState.AddModelError("", "Пользователь " + model.UserName + " не найден.");
            }
            return View(model);
        }

        /// <summary>
        /// Выйти из учетной записи.
        /// </summary>
        public async System.Threading.Tasks.Task<IActionResult> LogOffAsync()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
