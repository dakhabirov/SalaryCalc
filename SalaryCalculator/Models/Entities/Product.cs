using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator.Models
{
    public class Product
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Display(Name="Наименование")]
        public string ProductName { get; set; }

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
        [Display(Name = "Товар YOTA")]
        public bool IsFavorite { get; set; }
    }
}
