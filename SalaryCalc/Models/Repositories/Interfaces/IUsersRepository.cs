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
        /// <param name="Id">Идентификатор пользователя.</param>
        /// <returns>Пользователь.</returns>
        User GetUserById(string Id);

        /// <summary>
        /// Получить пользователя по его имени.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Пользователь.</returns>
        User GetUserByUserName(string userName);

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
