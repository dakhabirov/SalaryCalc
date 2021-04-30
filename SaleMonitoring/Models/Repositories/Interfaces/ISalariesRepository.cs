using SalaryCalc.Models.Entities;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface ISalariesRepository
    {
        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника.</param>
        /// <param name="sum">Сумма заработной платы.</param>
        /// <param name="date">Дата выплаты заработной платы.</param>
        void SaveSalary(string userId, double sum, ushort year, byte month);

        /// <summary>
        /// Добавить заработную плату.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника.</param>
        /// <param name="sum">Сумма заработной платы.</param>
        /// <param name="date">Дата выплаты заработной платы.</param>
        void CreateSalary(string userId, double sum, ushort year, byte month);

        /// <summary>
        /// Обновить заработную плату.
        /// </summary>
        /// <param name="salary">Заработная плата.</param>
        /// <param name="sum">Сумма заработной платы.</param>
        /// <param name="date">Дата выплаты заработной платы.</param>
        void UpdateSalary(Salary salary, double sum, ushort year, byte month);

        /// <summary>
        /// Удалить заработную плату.
        /// </summary>
        /// <param name="id">Идентификатор заработной платы.</param>
        void DeleteSalary(Guid id);

        /// <summary>
        /// Получить все заработные платы сотрудника.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Набор заработных плат.</returns>
        IQueryable<Salary> GetSalaries(string userId);

        /// <summary>
        /// Получить заработную плату по дате выплаты.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника.</param>
        /// <param name="salaryDate">Дата выплаты заработной платы.</param>
        /// <returns>Заработная плата.</returns>
        Salary GetSalaryByDate(string userId, ushort year, byte month);
    }
}
