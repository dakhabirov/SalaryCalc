using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator.Models
{
    public class User: IdentityUser
    {
        /// <summary>
        /// Полное имя.
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "Полное имя")]
        public string Fullname { get; set; }

        /// <summary>
        /// Должность. 
        /// </summary>
        [Display(Name = "Должность")]
        public virtual Position Position { get; set; }

        /// <summary>
        /// Коллекция заработных плат.
        /// </summary>
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
