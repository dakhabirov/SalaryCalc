using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using System;
using System.Linq;

namespace SalaryCalc.Controllers
{
    [Authorize(Policy = "manager")]
    public class SalesController : Controller
    {
        private readonly AppDbContext context;
        private readonly DataManager dataManager;

        public SalesController(DataManager dataManager, AppDbContext context)
        {
            this.context = context;
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
            MultiSelectList products = new MultiSelectList(dataManager.Products.GetProducts(), "Id", "Name");
            ViewBag.Products = products;
            return View(sale);
        }

        [HttpPost]
        public IActionResult Edit(Sale sale, Guid[] productIds)
        {
            if (ModelState.IsValid)
            {
                dataManager.Sales.SaveSale(sale);
                foreach (Guid productId in productIds)
                {
                    dataManager.SaleProducts.SaveSaleProducts(sale.Id, productId, 1);
                }
                context.SaveChanges();
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
