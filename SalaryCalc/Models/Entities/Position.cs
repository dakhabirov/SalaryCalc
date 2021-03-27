using SalaryCalc.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
{
    public class Position : EntityBase
    {
        /// <summary>
        /// Наименование должности.
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "Заполните название должности")]
        [Display(Name = "Должность")]
        public string Name { get; set; }

        [Display(Name = "Часовая ставка")]
        public float HourlyRate { get; set; }
    }
}
