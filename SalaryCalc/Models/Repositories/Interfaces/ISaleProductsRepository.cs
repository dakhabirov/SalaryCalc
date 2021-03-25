using SalaryCalc.Models.Entities;
using System;
using System.Collections.Generic;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface ISaleProductsRepository
    {
        /// <summary>
        /// Получить все проданные товары.
        /// </summary>
        /// <param name="saleId">Идентификатор продажи.</param>
        /// <returns>Набор товаров.</returns>
        IEnumerable<SaleProduct> GetSaleProducts(Guid saleId);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="saleId">Идентификатор продажи.</param>
        /// <param name="productId">Идентификатор товара.</param>
        /// <param name="amount">Количество товара.</param>
        void SaveSaleProducts(Guid saleId, Guid productId, int amount);
    }
}
