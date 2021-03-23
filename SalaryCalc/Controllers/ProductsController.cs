using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using System;
using System.IO;

namespace SalaryCalc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductsController(DataManager dataManager, IWebHostEnvironment hostEnvironment)
        {
            this.dataManager = dataManager;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                return View("Show", dataManager.Products.GetProductById(id));
            }

            return View(dataManager.Products.GetProducts());
        }

        [Authorize(Policy = "Administrator")]
        public IActionResult Edit(Guid id)
        {
            var product = id == default ? new Product() : dataManager.Products.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public IActionResult Edit(Product product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    product.ImagePath = imageFile.FileName;
                    using var stream = new FileStream(Path.Combine(hostEnvironment.WebRootPath, "img/products/", imageFile.FileName), FileMode.Create);
                    imageFile.CopyTo(stream);
                }

                dataManager.Products.SaveProduct(product);
                return RedirectToAction(nameof(ProductsController.Index));
            }
            return View(product);
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public IActionResult Delete(Guid id)
        {
            dataManager.Products.DeleteProduct(id);
            return RedirectToAction(nameof(ProductsController.Index));
        }
    }
}
