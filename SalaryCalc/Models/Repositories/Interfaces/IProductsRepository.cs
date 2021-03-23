using SalaryCalc.Models.Entities;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        /// <summary>
        /// Получить все товары.
        /// </summary>
        /// <returns>Набор товаров.</returns>
        IQueryable<Product> GetProducts();

        /// <summary>
        /// Получить товар по идентификатору.
        /// </summary>
        /// <param name="Id">Идентификатор товара.</param>
        /// <returns>Товар.</returns>
        Product GetProductById(Guid Id);

        /// <summary>
        /// Получить товар по его наименованию.
        /// </summary>
        /// <param name="Name">Наименование товара.</param>
        /// <returns>Товар.</returns>
        Product GetProductByName(string Name);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="product">Объект класса Product.</param>
        void SaveProduct(Product product);

        /// <summary>
        /// Удалить товар.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        void DeleteProduct(Guid id);
    }
}
