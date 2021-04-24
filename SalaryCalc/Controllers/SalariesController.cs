using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
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
            var currentUserId = dataManager.Users.GetCurrentUserId();

            if (year != default & month != default)
            {
                Salary salary = dataManager.Users.GetSalaryByDate(currentUserId, year, month);
                ViewBag.Sales = dataManager.Sales.GetSalesByDate(year, month);
                return View("Show", salary);
            }

            var currentYear = (ushort)DateTime.Now.Year;
            var currentMonth = (byte)DateTime.Now.Month;

            var currentSalary = dataManager.Salaries.GetSalaryByDate(currentUserId, currentYear, currentMonth);
            if (currentSalary != default)
                ViewBag.CurrentSalary = currentSalary.Sum;
            else
                ViewBag.CurrentSalary = 0;

            return View(dataManager.Users.GetSalaries(dataManager.Users.GetCurrentUserId()));
        }
    }
}
