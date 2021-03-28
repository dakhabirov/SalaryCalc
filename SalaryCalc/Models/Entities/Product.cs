using SalaryCalc.Domain.Entities;
using SalaryCalc.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
{
    public class Product : EntityBase
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        [Display(Name = "Изображение")]
        public string ImagePath { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name="Цена")]
        public float Price { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Список проданных товаров.
        /// </summary>
        public List<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
    }
}
