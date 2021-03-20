using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalculator.Models;

namespace SalaryCalculator.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Administrator")]
    public class UsersController : Controller
    {
        private readonly DataManager dataManager;

        public UsersController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(dataManager.Users.GetUsers());
        }

        public IActionResult Edit(string id)
        {
            var user = id == null ? new User() : dataManager.Users.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                dataManager.Users.SaveUser(user);
                return RedirectToAction(nameof(HomeController.Index));
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            dataManager.Users.DeleteUser(id);
            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}