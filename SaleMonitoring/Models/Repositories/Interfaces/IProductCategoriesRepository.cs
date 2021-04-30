using SalaryCalc.Models.Entities;
using System.Linq;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface IProductCategoriesRepository
    {
        /// <summary>
        /// Получить все категории.
        /// </summary>
        /// <returns>Набор категорий.</returns>
        IQueryable<Category> GetCategories();
    }
}
