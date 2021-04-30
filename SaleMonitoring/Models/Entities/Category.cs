using SalaryCalc.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
{
    [Display(Name = "Категория")]
    public class Category : EntityBase
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        [Display(Name = "Категория")]
        public string Name { get; set; }

        /// <summary>
        /// Определяет, является ли категория особенной (избранной).
        /// </summary>
        [Display(Name = "Избранная категория")]
        public bool IsFavorite { get; set; }
    }
}
