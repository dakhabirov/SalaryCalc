using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using System;
using System.Linq;

namespace SalaryCalc.Controllers
{
    [Authorize(Policy = "user")]
    public class SalesController : Controller
    {
        private readonly AppDbContext context;
        private readonly DataManager dataManager;
        private readonly UserManager<User> userManager;

        public SalesController(AppDbContext context, DataManager dataManager, UserManager<User> userManager)
        {
            this.context = context;
            this.dataManager = dataManager;
            this.userManager = userManager;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default)  // если в запросе был указан id продажи
            {
                return View("Show", dataManager.Sales.GetSaleById(id)); // открываем представление Show и передаем туда выбранную продажу
            }

            return View(dataManager.Sales.GetSales()); // передаем список продаж
        }

        public IActionResult Edit(Guid id)
        {
            var sale = id == default ? new Sale() : dataManager.Sales.GetSaleById(id);
            MultiSelectList products = new MultiSelectList(context.Products, "Id", "Name");
            ViewBag.Products = products;
            return View(sale);
        }

        [HttpPost]
        public IActionResult Edit(Sale sale, Guid[] productIds)
        {
            if (ModelState.IsValid)
            {
                User currentUser = userManager.GetUserAsync(HttpContext.User).Result;
                sale.User = currentUser;
                dataManager.Sales.SaveSale(sale);
                foreach (Guid productId in productIds)
                {
                    dataManager.SaleProducts.SaveSaleProducts(sale, productId, 1);
                    Product product = dataManager.Products.GetProductById(productId);
                    dataManager.Salaries.SaveSalary(sale.UserId, product.Price * 0.05, (ushort)sale.DateAdded.Year, (byte)sale.DateAdded.Month);
                }
                return RedirectToAction(nameof(SalesController.Index));
            }
            return View(sale);
        }

        [Authorize(Policy = "administrator")]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            //
            // Обновляем заработную плату.
            //
            var sale = dataManager.Sales.GetSaleById(id);
            var salary = dataManager.Salaries.GetSalaries(sale.UserId)
                .Where(s => s.Month == sale.DateAdded.Month && s.Year == sale.DateAdded.Year)
                .FirstOrDefault();
            if (salary != default)
            {
                var saleProducts = dataManager.SaleProducts.GetSaleProducts(id);
                double sum = 0;  // Сумма проданных товаров.
                foreach (var sp in saleProducts)
                {
                    sum += sp.Product.Price * 0.05;
                }
                dataManager.Salaries.UpdateSalary(salary, salary.Sum - sum, salary.Year, salary.Month);
            }

            context.Sales.Remove(sale);
            context.SaveChanges();
            return RedirectToAction(nameof(SalesController.Index));
        }
    }
}
