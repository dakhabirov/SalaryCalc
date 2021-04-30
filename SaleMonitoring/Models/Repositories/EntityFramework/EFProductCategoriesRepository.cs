using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFProductCategoriesRepository : IProductCategoriesRepository
    {
        private readonly AppDbContext context;

        public EFProductCategoriesRepository(AppDbContext context)
        {
            this.context = context;
        }
        
        public IQueryable<Category> GetCategories()
        {
            return context.Categories;
        }
    }
}
