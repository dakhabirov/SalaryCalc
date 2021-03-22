using System;

namespace SalaryCalc.Models
{
    public class Salary
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сумма заработной платы.
        /// </summary>
        public double Sum { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Дата выплаты заработной платы.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
