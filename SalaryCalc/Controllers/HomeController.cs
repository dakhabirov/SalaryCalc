using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;

namespace SalaryCalc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index(User user)
        {
            ViewBag.Name = User.Identity.Name;
            return View(user.Salaries);
        }
    }
}