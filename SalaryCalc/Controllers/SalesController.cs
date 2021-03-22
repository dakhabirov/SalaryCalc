using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;
using System;

namespace SalaryCalc.Controllers
{
    [Authorize(Policy = "manager")]
    public class SalesController : Controller
    {
        private readonly DataManager dataManager;

        public SalesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        
        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                return View("Show", dataManager.Sales.GetSaleById(id));
            }

            return View(dataManager.Sales.GetSales());
        }

        public IActionResult Edit(Guid id)
        {
            var sale = id == default ? new Sale() : dataManager.Sales.GetSaleById(id);
            return View(sale);
        }

        [HttpPost]
        public IActionResult Edit(Sale sale)
        {
            if (ModelState.IsValid)
            {
                dataManager.Sales.SaveSale(sale);
                return RedirectToAction(nameof(SalesController.Index));
            }
            return View(sale);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            dataManager.Sales.DeleteSale(id);
            return RedirectToAction(nameof(SalesController.Index));
        }
    }
}
