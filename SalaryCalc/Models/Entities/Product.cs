using SalaryCalc.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
{
    public class Product : EntityBase
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        [Display(Name = "Изображение")]
        public string ImagePath { get; set; }

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
        public List<SaleProduct> SaleProducts { get; set; }

        public Product()
        {
            SaleProducts = new List<SaleProduct>();
        }
    }
}
