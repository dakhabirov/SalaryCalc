using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;
using System;

namespace SalaryCalc.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class ProductsController : Controller
    {
        private readonly DataManager dataManager;

        public ProductsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                return View("Show", dataManager.Products.GetProductById(id));
            }
 
            return View(dataManager.Products.GetProducts());
        }

        public IActionResult Edit(Guid id)
        {
            var product = id == null ? new Product() : dataManager.Products.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                dataManager.Products.SaveProduct(product);
                return RedirectToAction(nameof(ProductsController.Index));
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            dataManager.Products.DeleteProduct(id);
            return RedirectToAction(nameof(ProductsController.Index));
        }
    }
}
