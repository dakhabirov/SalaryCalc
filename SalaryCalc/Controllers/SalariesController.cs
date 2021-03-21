using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCalc.Controllers
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
