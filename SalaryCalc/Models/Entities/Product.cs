using SalaryCalc.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
{
    public class Product : EntityBase
    {
        /// <summary>
        /// Категория.
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "Категория")]
        public string Category { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [Display(Name="Цена")]
        public float Price { get; set; }

        /// <summary>
        /// Определяет, является ли товар избранным.
        /// </summary>
        [Display(Name = "Избранный товар")]
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Коллекция проданных товаров.
        /// </summary>
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
