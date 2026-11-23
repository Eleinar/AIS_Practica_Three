namespace DataAccessLayer
{
    public class ConnectionSettings
    {
        /// <summary>
        /// Возвращает строку подключения к PostgreSQL
        /// </summary>
        /// <returns>Строка из переменной окружения CAR_CONNECTION или значение по умолчанию</returns>
        public static string DBConnection()
        {
            return Environment.GetEnvironmentVariable("CAR_CONNECTION") ?? "Host=192.168.31.243;Port=5432;Database=car_base;Username=trash;Password=123";
        }
    }
}
