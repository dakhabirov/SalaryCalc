using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SalaryCalc.Controllers
{
    [Authorize(Policy = "manager")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext context;
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductsController(AppDbContext context, DataManager dataManager, IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
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
            SelectList categories = new SelectList(context.Categories, "Id", "Name");
            ViewBag.Categories = categories;
            return View(product);
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> EditAsync(Product product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    product.ImagePath = imageFile.FileName;
                    using var stream = new FileStream(Path.Combine(hostEnvironment.WebRootPath, "img/products/", imageFile.FileName), FileMode.Create);
                    await imageFile.CopyToAsync(stream);
                }

                dataManager.Products.SaveProduct(product);
                return RedirectToAction(nameof(ProductsController.Index));
            }
            return View(product);
        }

        [Authorize(Policy = "Administrator")]
        public IActionResult Delete(Guid id)
        {
            dataManager.Products.DeleteProduct(id);
            return RedirectToAction(nameof(ProductsController.Index));
        }
    }
}
