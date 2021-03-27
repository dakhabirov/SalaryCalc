using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models.Entities;
using SalaryCalc.ViewModels;
using System.Threading.Tasks;

namespace SalaryCalc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName, Fullname = model.Fullname, Position = model.Position };
                // добавляем пользователя
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
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
                var user = await userManager.FindByNameAsync(model.UserName);  // ищем пользователя по введенному логину

                // если пользователь найден
                if (user != null)
                {
                    // проверяем введенный пароль
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember, false);
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
            await signInManager.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
