using SalaryCalc.Models.Entities;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        /// <summary>
        /// Получить все категории.
        /// </summary>
        /// <returns>Набор категорий.</returns>
        IQueryable<Category> GetCategories();

        /// <summary>
        /// Получить категорию по идентификатору.
        /// </summary>
        /// <param name="Id">Идентификатор категории.</param>
        /// <returns>Категори.</returns>
        Category GetCategoryById(Guid Id);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="category">Объект класса Category.</param>
        void SaveCategory(Category category);

        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        void DeleteCategory(Guid id);
    }
}
