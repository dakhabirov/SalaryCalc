using System;
using System.Collections.Generic;

namespace SalaryCalc.Models
{
    /// <summary>
    /// Продажа.
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата продажи.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Коллекция проданных товаров.
        /// </summary>
        public virtual ICollection<SaleProduct> Sale_Products { get; set; }
    }
}
