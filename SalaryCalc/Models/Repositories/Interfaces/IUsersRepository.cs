using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <returns>Набор пользователей.</returns>
        IQueryable<User> GetUsers();

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователь.</returns>
        User GetUserById(string id);

        /// <summary>
        /// Получить пользователя по его имени.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Пользователь.</returns>
        User GetUserByUserName(string userName);

        /// <summary>
        /// Получить заработную плату по дате.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Заработная плата.</returns>
        Salary GetSalaryByDate(string userId, DateTime date);

        /// <summary>
        /// Получить все заработные платы сотрудника.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Набор заработных плат.</returns>
        IQueryable<Salary> GetSalaries(string id);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="user">Объект класса User.</param>
        void SaveUser(User user);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        void DeleteUser(string id);
    }
}
