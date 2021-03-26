using SalaryCalc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
{
    /// <summary>
    /// Продажа.
    /// </summary>
    public class Sale : EntityBase
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Коллекция проданных товаров.
        /// </summary>
        public List<SaleProduct> SaleProducts { get; set; }

        public Sale()
        {
            SaleProducts = new List<SaleProduct>();
        }
    }
}
