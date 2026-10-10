namespace DataAccessLayer
{
    public class ConnectionSettings
    {
        public static string DBConnection()
        {
            return Environment.GetEnvironmentVariable("CAR_CONNECTION") ?? "Host=100.102.81.5;Port=5432;Database=car_base;Username=eleainar;Password=123";
        }
    }
}
