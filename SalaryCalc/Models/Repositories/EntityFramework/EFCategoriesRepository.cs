using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFCategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext context;

        public EFCategoriesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> GetCategories()
        {
            return context.Categories;
        }

        public Category GetCategoryById(Guid Id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == Id);
        }

        public void SaveCategory(Category category)
        {
            context.Entry(category).State = category.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            context.Categories.Remove(new Category() { Id = id });
            context.SaveChanges();
        }
    }
}
