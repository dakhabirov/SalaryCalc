using SalaryCalculator.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator.Models
{
    public class Position : EntityBase
    {
        /// <summary>
        /// Наименование должности.
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "Заполните название должности")]
        [Display(Name = "Название должности")]
        public override string Name { get; set; }

        [Display(Name = "Часовая ставка")]
        public float HourlyRate { get; set; }
    }
}
