using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.Interfaces
{
    public interface IPositionsRepository
    {
        /// <summary>
        /// Получить все должности.
        /// </summary>
        /// <returns>Набор должностей.</returns>
        IQueryable<Position> GetPositions();

        /// <summary>
        /// Получить должность по идентификатору.
        /// </summary>
        /// <param name="Id">Идентификатор должности.</param>
        /// <returns>Должность.</returns>
        Position GetPositionById(Guid Id);

        /// <summary>
        /// Получить должность по его наименованию.
        /// </summary>
        /// <param name="Name">Наименование должности.</param>
        /// <returns>Должность.</returns>
        Position GetPositionByName(string Name);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="product">Объект класса Position.</param>
        void SavePosition(Position position);

        /// <summary>
        /// Удалить должность.
        /// </summary>
        /// <param name="id">Идентификатор должности.</param>
        void DeletePosition(Guid Id);
    }
}
