using System;

namespace SalaryCalculator.Models
{
    public class Salary
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата выплаты заработной платы.
        /// </summary>
        public DateTime SalaryDate { get; set; }
    }
}
