using SalaryCalc.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models
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
        [Display(Name = "Товар YOTA")]
        public bool IsFavorite { get; set; }
    }
}
