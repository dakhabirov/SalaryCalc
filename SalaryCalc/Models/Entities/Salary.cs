using SalaryCalc.Domain.Entities;
using System;

namespace SalaryCalc.Models.Entities
{
    public class Salary : EntityBase
    {
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
