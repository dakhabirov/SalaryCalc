using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFSalesRepository : ISalesRepository
    {
        private readonly AppDbContext context;

        public EFSalesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Sale> GetSales()
        {
            context.Users.ToList();  // список продавцов
            return context.Sales;
        }

        public Sale GetSaleById(Guid Id)
        {
            context.Products.Include(s => s.SaleProducts)
                                        .ThenInclude(sp => sp.Product)
                                        .ToList();  // список товаров в продаже

            return context.Sales.FirstOrDefault(s => s.Id == Id);
        }

        public List<Sale> GetSalesByDate(ushort year, byte month)
        {
            return context.Sales.Where(s => s.DateAdded.Year == year & s.DateAdded.Month == month).ToList();
        }

        public void SaveSale(Sale sale)
        {
            context.Entry(sale).State = sale.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteSale(Guid id)
        {
            context.Sales.Remove(new Sale() { Id = id });
            context.SaveChanges();
        }
    }
}
