using SalaryCalc.Models.Entities;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface ISalesRepository
    {
        /// <summary>
        /// Получить все продажи.
        /// </summary>
        /// <returns>Набор продаж.</returns>
        IQueryable<Sale> GetSales();

        /// <summary>
        /// Получить продажу по идентификатору.
        /// </summary>
        /// <param name="Id">Идентификатор продажи.</param>
        /// <returns>Продажа.</returns>
        Sale GetSaleById(Guid Id);

        /// <summary>
        /// Получить продажу по дате.
        /// </summary>
        /// <param name="saleDate">Дата продажи.</param>
        /// <returns>Продажа.</returns>
        Sale GetSaleBySaleDate(DateTime saleDate);

        /// <summary>
        /// Получить пользователя, который реализовал продажу.
        /// </summary>
        /// <param name="sale">Продажа.</param>
        /// <returns>Пользователь.</returns>
        User GetSaleUser(Sale sale);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="sale">Объект класса Sale.</param>
        void SaveSale(Sale sale);

        /// <summary>
        /// Удалить продажу.
        /// </summary>
        /// <param name="id">Идентификатор продажи.</param>
        void DeleteSale(Guid id);
    }
}
