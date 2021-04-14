namespace SalaryCalc.Service
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

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public static string Phone { get; set; }

        /// <summary>
        /// Электронный адрес.
        /// </summary>
        public static string Email { get; set; }
    }
}
