using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() => DateAdded = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Дата добавления")]
        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
