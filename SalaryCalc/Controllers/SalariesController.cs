using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCalculator.Controllers
{
    [Authorize(Policy = "Manager")]
    public class SalariesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
