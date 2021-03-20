using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCalculator.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Administrator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
