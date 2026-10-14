using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public static class LogicFactory
    {
        /// <summary>
        /// Создаёт Logic с репозиториями Entity Framework
        /// </summary>
        /// <param name="connectionString">Строка подключения или null для значения по умолчанию</param>
        /// <returns>Экземпляр бизнес-логики</returns>
        public static Logic CreateEF(string? connectionString = null)
        {
            string cs = connectionString ?? ConnectionSettings.DBConnection();
            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(cs)
                .Options;

            return new Logic(
                new EntityRepository<Car, string>(options),
                new EntityRepository<Brand, int>(options));
        }

        /// <summary>
        /// Создаёт Logic с репозиториями Dapper
        /// </summary>
        /// <param name="connectionString">Строка подключения или null для значения по умолчанию</param>
        /// <returns>Экземпляр бизнес-логики</returns>
        public static Logic CreateDapper(string? connectionString = null)
        {
            string cs = connectionString ?? ConnectionSettings.DBConnection();
            return new Logic(
                new DapperRepository<Car, string>(cs),
                new DapperRepository<Brand, int>(cs));
        }
    }
}
