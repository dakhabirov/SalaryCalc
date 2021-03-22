using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models
{
    public class Position
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование должности.
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "Заполните название должности")]
        [Display(Name = "Название должности")]
        public string Name { get; set; }

        [Display(Name = "Часовая ставка")]
        public float HourlyRate { get; set; }
    }
}
