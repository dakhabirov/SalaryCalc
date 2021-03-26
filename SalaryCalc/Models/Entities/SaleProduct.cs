using System;

namespace SalaryCalc.Models.Entities
{
    public class SaleProduct
    {
        ///// <summary>
        ///// Идентификатор.
        ///// </summary>
        //public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор продажи.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Продажа. 
        /// </summary>
        public virtual Sale Sale { get; set; }

        /// <summary>
        /// Товар. 
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
