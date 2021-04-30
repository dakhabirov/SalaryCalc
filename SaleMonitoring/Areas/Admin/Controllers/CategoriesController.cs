using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using System;

namespace SalaryCalc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Administrator")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext context;
        private readonly DataManager dataManager;

        public CategoriesController(AppDbContext context, DataManager dataManager)
        {
            this.context = context;
            this.dataManager = dataManager;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                return View("Show", dataManager.Categories.GetCategoryById(id));
            }

            return View(dataManager.Categories.GetCategories());
        }

        public IActionResult Edit(Guid id)
        {
            var category = id == default ? new Category() : dataManager.Categories.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                dataManager.Categories.SaveCategory(category);
                return RedirectToAction(nameof(CategoriesController.Index));
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            dataManager.Categories.DeleteCategory(id);
            return RedirectToAction(nameof(CategoriesController.Index));
        }
    }
}
