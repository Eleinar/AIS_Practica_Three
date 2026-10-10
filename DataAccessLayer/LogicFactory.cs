using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    internal class LogicFactory
    {
        public static Logic CreateEF(string connectionString)
        {
            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connectionString).Options;

            return new Logic(new EntityRepository<Car, string>(options),
                                new EntityRepository<Brand, int>(options));
        }

        public static Logic CreateDapper(string connectionString)
        {
            return new Logic(new DapperRepository<Car, string>(connectionString),
                                new DapperRepository<Brand, int>(connectionString));
        }
    }
}
