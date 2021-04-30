using SalaryCalc.Models.Entities;
using System;
using System.Collections.Generic;
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
        /// <param name="year">Год.</param>
        /// <param name="month">Год.</param>
        /// <returns>Продажа.</returns>
        List<Sale> GetSalesByDate(ushort year, byte month);

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
