using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalc.Controllers
{
    [Authorize(Policy = "manager")]
    public class SalesController : Controller
    {
        private readonly AppDbContext context;
        private readonly DataManager dataManager;
        private readonly UserManager<User> userManager;

        public SalesController(DataManager dataManager, AppDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.dataManager = dataManager;
            this.userManager = userManager;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default)  // если в запросе был указан id товара
            {
                var products = context.Products.Include(s => s.SaleProducts)
                                        .ThenInclude(sp => sp.Product)
                                        .ToList();  // ищем товары в продаже
                ViewBag.Products = products;    // передаем эти товары в представление через ViewBag
                return View("Show", dataManager.Sales.GetSaleById(id)); // открываем представление Show и передаем туда выбранную продажу
            }

            var sales = dataManager.Sales.GetSales();   // извлекаем все продажи
            var users = new List<User>();   // сюда будем загружать продавцов
   
            foreach (Sale sale in sales)    // загружаем продавцов в список
            {
                users.Add(dataManager.Sales.GetSaleUser(sale));
            }

            ViewBag.Users = users;  // передаем список продацов через ViewBag
            return View(sales); // передаем список продаж
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
                User currentUser = userManager.GetUserAsync(HttpContext.User).Result;
                sale.User = currentUser;
                dataManager.Sales.SaveSale(sale);
                foreach (Guid productId in productIds)
                {
                    dataManager.SaleProducts.SaveSaleProducts(sale, productId, 1);
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
