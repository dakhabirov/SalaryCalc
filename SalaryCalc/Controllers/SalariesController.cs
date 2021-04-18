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

        public IActionResult Index(ushort year, byte month)
        {
            if (year != default)
            {
                return View("Show", dataManager.Users.GetSalaryByDate(dataManager.Users.GetCurrentUserId(), year, month));
            }

            var currentYear = (ushort)DateTime.Now.Year;
            var currentMonth = (byte)DateTime.Now.Month;

            var currentSalary = dataManager.Salaries.GetSalaryByDate(dataManager.Users.GetCurrentUserId(), currentYear, currentMonth);
            ViewBag.CurrentSalary = currentSalary.Sum;

            return View(dataManager.Users.GetSalaries(dataManager.Users.GetCurrentUserId()));
        }
    }
}
