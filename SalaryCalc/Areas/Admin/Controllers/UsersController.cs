using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using System;
using System.Linq;

namespace SalaryCalc.Controllers
{
    [Area("admin")]
    [Authorize(Policy = "Administrator")]
    public class UsersController : Controller
    {
        private readonly AppDbContext context;
        private readonly DataManager dataManager;
        private readonly UserManager<User> userManager;

        public UsersController(DataManager dataManager, AppDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.dataManager = dataManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(dataManager.Users.GetUsers());
        }

        public IActionResult Edit(string id)
        {
            var user = id == default ? new User() : dataManager.Users.GetUserById(id);
            SelectList positions = new SelectList(context.Positions, "Id", "Name"); //, user.Position.Id);
            ViewBag.Positions = positions;
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user, Guid PositionId)
        {
            if (ModelState.IsValid)
            {
                var position = context.Positions.FirstOrDefault(p => p.Id == PositionId);
                user.Position = position;
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