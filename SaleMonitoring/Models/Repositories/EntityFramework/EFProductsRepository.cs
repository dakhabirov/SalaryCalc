using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly AppDbContext context;

        public EFProductsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> GetProducts()
        {
            return context.Products;
        }

        public Product GetProductById(Guid Id)
        {
            context.Products.Where(p => p.Id == Id)
                                .Include(c => c.Category)
                                    .FirstOrDefault();    // подгружаем категорию из базы данных в контекст
            return context.Products.Find(Id);
        }

        public Product GetProductByName(string Name)
        {
            return context.Products.FirstOrDefault(p => p.Name == Name);
        }

        public void SaveProduct(Product product)
        {
            context.Entry(product).State = product.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteProduct(Guid id)
        {
            context.Products.Remove(new Product() { Id = id });
            context.SaveChanges();
        }
    }
}
