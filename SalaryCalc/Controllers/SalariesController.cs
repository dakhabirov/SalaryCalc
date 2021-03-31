using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;
using System;

namespace SalaryCalc.Controllers
{
    [Authorize]
    public class SalariesController : Controller
    {
        private readonly DataManager dataManager;

        public SalariesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index(string userId, DateTime date)
        {
            if (date != default)
            {
                return View("Show", dataManager.Users.GetSalaryByDate(userId, date));
            }

            return View(dataManager.Users.GetSalaries(userId));
        }
    }
}
