using SalaryCalc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Models.Entities
{
    public class Position : EntityBase
    {
        /// <summary>
        /// Наименование должности.
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Должность")]
        public string Name { get; set; }

        [Display(Name = "Часовая ставка")]
        public float HourlyRate { get; set; }

        /// <summary>
        /// Список пользователей, которым принадлежит данная должность.
        /// </summary>
        public List<User> Users { get; set; } = new List<User>();
    }
}
