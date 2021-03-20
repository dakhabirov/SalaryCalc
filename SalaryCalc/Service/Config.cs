

namespace SalaryCalculator.Service
{
    /// <summary>
    /// Конфигурация.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Название компании.
        /// </summary>
        public static string CompanyName { get; set; }
    }
}
