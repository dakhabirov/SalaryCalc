using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator.Domain.Entities
{
    public abstract class EntityBase
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Наименование")]
        public virtual string Name { get; set; }

        [Display(Name = "Изображение")]
        public virtual string ImagePath { get; set; }
    }
}
