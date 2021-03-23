using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
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
        [Display(Name = "Дата и время продажи")]
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User User { get; set; }
    }
}
