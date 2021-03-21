using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCalc.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Policy = "Administrator")]
    public class PositionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
