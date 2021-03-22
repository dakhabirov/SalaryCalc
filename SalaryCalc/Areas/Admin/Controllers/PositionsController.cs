using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;
using System;

namespace SalaryCalc.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Policy = "Administrator")]
    public class PositionsController : Controller
    {
        private readonly DataManager dataManager;

        public PositionsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                return View("Show", dataManager.Positions.GetPositionById(id));
            }

            return View(dataManager.Positions.GetPositions());
        }

        public IActionResult Edit(Guid id)
        {
            var position = id == default ? new Position() : dataManager.Positions.GetPositionById(id);
            return View(position);
        }

        [HttpPost]
        public IActionResult Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                dataManager.Positions.SavePosition(position);
                return RedirectToAction(nameof(PositionsController.Index));
            }
            return View(position);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            dataManager.Positions.DeletePosition(id);
            return RedirectToAction(nameof(PositionsController.Index));
        }
    }
}
