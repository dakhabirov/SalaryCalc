using System;

namespace SalaryCalculator.Models
{
    /// <summary>
    /// Проданные товары или услуги.
    /// </summary>
    public class SaleProduct
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Товар. 
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
