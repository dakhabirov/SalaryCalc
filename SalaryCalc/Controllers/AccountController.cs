using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using SalaryCalc.ViewModels;
using System;
using System.Threading.Tasks;

namespace SalaryCalc.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext context;
        private readonly DataManager dataManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(AppDbContext context, DataManager dataManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.dataManager = dataManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            SelectList positions = new SelectList(context.Positions, "Id", "Name"); // должности
            ViewBag.Positions = positions;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(context.Positions, "Id", "Name");
                return View(model);
            }
            else
            {
                User user = new User { UserName = model.UserName, Fullname = model.Fullname, PositionId = model.PositionId };
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
                return View(model);
            }
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)    // проверяем введенные данные на валидность
            {
                var user = await userManager.FindByNameAsync(model.UserName);  // ищем пользователя по введенному логину

                // если пользователь найден
                if (user != null)
                {
                    // проверяем введенный пароль
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(model.ReturnUrl ?? "/");
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
        public async Task<IActionResult> LogOffAsync()
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
